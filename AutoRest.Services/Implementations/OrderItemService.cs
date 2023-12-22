using AutoMapper;
using AutoRest.Common.Entity.InterfaceDB;
using AutoRest.Context.Contracts.Models;
using AutoRest.Repositories.Contracts;
using AutoRest.Services.Contracts.Exceptions;
using AutoRest.Services.Contracts.Interfaces;
using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.ModelsRequest;

namespace AutoRest.Services.Implementations
{
    public class OrderItemService : IOrderItemService, IServiceAnchor
    {
        private readonly IOrderItemReadRepository orderItemReadRepository;
        private readonly IOrderItemWriteRepository orderItemWriteRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly ITableReadRepository tableReadRepository;
        private readonly IMenuItemReadRepository menuItemReadRepository;
        private readonly ILoyaltyCardReadRepository loyaltyCardReadRepository;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderItemService(IOrderItemReadRepository orderItemReadRepository,
            IOrderItemWriteRepository orderItemWriteRepository,
            IEmployeeReadRepository employeeReadRepository,
            ITableReadRepository tableReadRepository,
            IMenuItemReadRepository menuItemReadRepository,
            ILoyaltyCardReadRepository loyaltyCardReadRepository,
            IPersonReadRepository personReadRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.orderItemReadRepository = orderItemReadRepository;
            this.orderItemWriteRepository = orderItemWriteRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.tableReadRepository = tableReadRepository;
            this.menuItemReadRepository = menuItemReadRepository;
            this.loyaltyCardReadRepository = loyaltyCardReadRepository;
            this.personReadRepository = personReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<OrderItemModel>> IOrderItemService.GetAllAsync( CancellationToken cancellationToken)
        {
            var orderItems = await orderItemReadRepository.GetAllAsync(cancellationToken);

            var employeeWaiterIds = orderItems.Select(x => x.EmployeeWaiterId).Distinct();
            var tableIds = orderItems.Select(x => x.TableId).Distinct();
            var menuItemIds = orderItems.Select(x => x.MenuItemId).Distinct();
            var loyaltyCardIds = orderItems.Where(x => x.LoyaltyCardId.HasValue)
                .Select(x => x.LoyaltyCardId!.Value)
                .Distinct();
            var employeeCashierIds = orderItems.Where(x => x.EmployeeCashierId.HasValue)
                .Select(x => x.EmployeeCashierId!.Value)
                .Distinct();

            var employeeWaiterDictionary = await employeeReadRepository.GetPersonByEmployeeIdsAsync(employeeWaiterIds, cancellationToken);
            var tableDictionary = await tableReadRepository.GetByIdsAsync(tableIds, cancellationToken);
            var menuItemDictionary = await menuItemReadRepository.GetByIdsAsync(menuItemIds, cancellationToken);
            var loyaltyCardDictionary = await loyaltyCardReadRepository.GetByIdsAsync(loyaltyCardIds, cancellationToken);
            var employeeCashierDictionary = await employeeReadRepository.GetPersonByEmployeeIdsAsync(employeeCashierIds, cancellationToken);

            var listOrderItemModel = new List<OrderItemModel>();
            foreach (var orderItem in orderItems)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (!employeeWaiterDictionary.TryGetValue(orderItem.EmployeeWaiterId, out var employeeWaiter))
                {
                    continue;
                }

                if (!tableDictionary.TryGetValue(orderItem.TableId, out var table))
                {
                    continue;
                }

                if (!menuItemDictionary.TryGetValue(orderItem.MenuItemId, out var menuItem))
                {
                    continue;
                }

                var order = mapper.Map<OrderItemModel>(orderItem);

                if (orderItem.LoyaltyCardId.HasValue)
                {
                    loyaltyCardDictionary.TryGetValue(orderItem.LoyaltyCardId.Value, out var loyaltyCard);
                    order.LoyaltyCard = mapper.Map<LoyaltyCardModel>(loyaltyCard);

                }

                if (orderItem.EmployeeCashierId.HasValue)
                {
                    employeeCashierDictionary.TryGetValue(orderItem.EmployeeCashierId.Value, out var employeeCashier);
                    order.EmployeeCashier = mapper.Map<PersonModel>(employeeCashier);
                }

                order.EmployeeWaiter = mapper.Map<PersonModel>(employeeWaiter);
                order.Table = mapper.Map<TableModel>(table);
                order.MenuItem = mapper.Map<MenuItemModel>(menuItem);


                listOrderItemModel.Add(order);
            }

            return listOrderItemModel;
        }

        async Task<OrderItemModel?> IOrderItemService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await orderItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            var employeeWaiter = await employeeReadRepository.GetByIdAsync(item.EmployeeWaiterId, cancellationToken);
            var personWaiter = await personReadRepository.GetByIdAsync(employeeWaiter!.PersonId, cancellationToken);
            var table = await tableReadRepository.GetByIdAsync(item.TableId, cancellationToken);
            var menuItem = await menuItemReadRepository.GetByIdAsync(item.MenuItemId, cancellationToken);

            var order = mapper.Map<OrderItemModel>(item);

            order.EmployeeWaiter = mapper.Map<PersonModel>(personWaiter);
            order.Table = mapper.Map<TableModel>(table);
            order.MenuItem = mapper.Map<MenuItemModel>(menuItem);

            if (item.LoyaltyCardId != null)
            {
                var loyaltyCard = await loyaltyCardReadRepository.GetByIdAsync(item.LoyaltyCardId.Value, cancellationToken);
                order.LoyaltyCard = loyaltyCard != null
                    ? mapper.Map<LoyaltyCardModel>(loyaltyCard)
                    : null;
            }

            if (item.EmployeeCashierId != null)
            {
                var employeeCashier = await employeeReadRepository.GetByIdAsync(item.EmployeeCashierId.Value, cancellationToken);
                var personCashier = await personReadRepository.GetByIdAsync(employeeCashier!.PersonId, cancellationToken);

                order.EmployeeCashier = personCashier != null
                    ? mapper.Map<PersonModel>(personCashier)
                    : null;
            }
            return order;
        }

        async Task<OrderItemModel> IOrderItemService.AddAsync(OrderItemRequestModel order, CancellationToken cancellationToken)
        {
            var item = new OrderItem
            {
                Id = Guid.NewGuid(),
                EmployeeWaiterId = order.EmployeeWaiterId,
                TableId = order.TableId,
                MenuItemId = order.MenuItemId,
                OrderStatus = order.OrderStatus
            };

            if (order.LoyaltyCardId != Guid.Empty)
            {
                item.LoyaltyCardId = order.LoyaltyCardId;
            }

            if (order.EmployeeCashierId != Guid.Empty)
            {
                item.EmployeeCashierId = order.EmployeeCashierId;
            }

            orderItemWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<OrderItemModel>(item);
        }

        async Task<OrderItemModel> IOrderItemService.EditAsync(OrderItemRequestModel source, CancellationToken cancellationToken)
        {
            var targetOrderItem = await orderItemReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetOrderItem == null)
            {
                throw new AutoRestEntityNotFoundException<OrderItem>(source.Id);
            }

            targetOrderItem.OrderStatus = source.OrderStatus;

            var employeeWaiter = await employeeReadRepository.GetByIdAsync(source.EmployeeWaiterId, cancellationToken);
            targetOrderItem.EmployeeWaiterId = employeeWaiter!.Id;
            targetOrderItem.EmployeeWaiter = employeeWaiter;

            var employeeCashier = await employeeReadRepository.GetByIdAsync(source.EmployeeCashierId.Value, cancellationToken);
            targetOrderItem.EmployeeCashierId = employeeCashier!.Id;
            targetOrderItem.EmployeeCashier = employeeCashier;

            var table = await tableReadRepository.GetByIdAsync(source.TableId, cancellationToken);
            targetOrderItem.TableId = table!.Id;
            targetOrderItem.Table = table;

            var menuItem = await menuItemReadRepository.GetByIdAsync(source.MenuItemId, cancellationToken);
            targetOrderItem.MenuItemId = menuItem!.Id;
            targetOrderItem.MenuItem = menuItem;

            var loyaltyCard = await loyaltyCardReadRepository.GetByIdAsync(source.LoyaltyCardId.Value, cancellationToken);
            targetOrderItem.LoyaltyCardId = loyaltyCard!.Id;
            targetOrderItem.LoyaltyCard = loyaltyCard;

            orderItemWriteRepository.Update(targetOrderItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<OrderItemModel>(targetOrderItem);
        }

        async Task IOrderItemService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetOrderItem = await orderItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetOrderItem == null)
            {
                throw new AutoRestEntityNotFoundException<OrderItem>(id);
            }
            if (targetOrderItem.DeletedAt.HasValue)
            {
                throw new AutoRestInvalidOperationException($"Заказ с идентификатором {id} уже удален");
            }

            orderItemWriteRepository.Delete(targetOrderItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
