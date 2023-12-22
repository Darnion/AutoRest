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
    public class MenuItemService : IMenuItemService, IServiceAnchor
    {
        private readonly IMenuItemReadRepository menuItemReadRepository;
        private readonly IMenuItemWriteRepository menuItemWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MenuItemService(IMenuItemReadRepository menuItemReadRepository,
            IMenuItemWriteRepository menuItemWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.menuItemReadRepository = menuItemReadRepository;
            this.menuItemWriteRepository = menuItemWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<MenuItemModel>> IMenuItemService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await menuItemReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<MenuItemModel>>(result);
        }

        async Task<MenuItemModel?> IMenuItemService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await menuItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<MenuItemModel>(item);
        }

        async Task<MenuItemModel> IMenuItemService.AddAsync(MenuItemRequestModel menuItem, CancellationToken cancellationToken)
        {
            var item = new MenuItem
            {
                Id = Guid.NewGuid(),
                Title = menuItem.Title,
                Cost = menuItem.Cost,
            };
            menuItemWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<MenuItemModel>(item);
        }

        async Task<MenuItemModel> IMenuItemService.EditAsync(MenuItemRequestModel source, CancellationToken cancellationToken)
        {
            var targetMenuItem = await menuItemReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetMenuItem == null)
            {
                throw new AutoRestEntityNotFoundException<MenuItem>(source.Id);
            }

            targetMenuItem.Title = source.Title;
            targetMenuItem.Cost = source.Cost;

            menuItemWriteRepository.Update(targetMenuItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<MenuItemModel>(targetMenuItem);
        }

        async Task IMenuItemService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetMenuItem = await menuItemReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetMenuItem == null)
            {
                throw new AutoRestEntityNotFoundException<MenuItem>(id);
            }
            if (targetMenuItem.DeletedAt.HasValue)
            {
                throw new AutoRestInvalidOperationException($"Позиция меню с идентификатором {id} уже удалена");
            }

            menuItemWriteRepository.Delete(targetMenuItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
