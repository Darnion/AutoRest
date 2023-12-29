using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using AutoRest.Api.Models;
using AutoRest.Api.Models.Enums;
using AutoRest.Api.ModelsRequest.MenuItem;
using AutoRest.Api.ModelsRequest.LoyaltyCard;
using AutoRest.Api.ModelsRequest.Employee;
using AutoRest.Api.ModelsRequest.Table;
using AutoRest.Api.ModelsRequest.Person;
using AutoRest.Api.ModelsRequest.OrderItem;
using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.Models.Enums;
using AutoRest.Services.Contracts.ModelsRequest;
using Microsoft.OpenApi.Extensions;

namespace AutoRest.Api.Infrastructures
{
    /// <summary>
    /// Профиль маппера АПИшки
    /// </summary>
    public class ApiAutoMapperProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiAutoMapperProfile"/>
        /// </summary>
        public ApiAutoMapperProfile()
        {
            CreateMap<LoyaltyCardTypesModel, LoyaltyCardTypesResponse>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
            CreateMap<EmployeeTypesModel, EmployeeTypesResponse>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<MenuItemRequest, MenuItemRequestModel>(MemberList.Destination);
            CreateMap<CreateMenuItemRequest, MenuItemRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<MenuItemModel, MenuItemResponse>(MemberList.Destination);

            CreateMap<LoyaltyCardRequest, LoyaltyCardRequestModel>(MemberList.Destination);
            CreateMap<CreateLoyaltyCardRequest, LoyaltyCardRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<LoyaltyCardModel, LoyaltyCardResponse>(MemberList.Destination);

            CreateMap<EmployeeRequest, EmployeeRequestModel>(MemberList.Destination);
            CreateMap<CreateEmployeeRequest, EmployeeRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<EmployeeModel, EmployeeResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Person != null
                    ? $"{x.Person.LastName} {x.Person.FirstName} {x.Person.Patronymic}"
                    : string.Empty))
                .ForMember(x => x.EmployeeType, opt => opt.MapFrom(x => x.EmployeeType));

            CreateMap<PersonRequest, PersonRequestModel>(MemberList.Destination);
            CreateMap<CreatePersonRequest, PersonRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<PersonModel, PersonResponse>(MemberList.Destination);

            CreateMap<TableRequest, TableRequestModel>(MemberList.Destination);
            CreateMap<CreateTableRequest, TableRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<TableModel, TableResponse>(MemberList.Destination);

            CreateMap<OrderItemRequest, OrderItemRequestModel>(MemberList.Destination);
            CreateMap<CreateOrderItemRequest, OrderItemRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, next => next.Ignore());
            CreateMap<OrderItemModel, OrderItemResponse>(MemberList.Destination)
                .ForMember(x => x.OrderAcceptTime, opt => opt.MapFrom(x => x.CreatedAt))
                .ForMember(x => x.EmployeeWaiterFIO, opt => opt.MapFrom(x => $"{x.EmployeeWaiter!.LastName} {x.EmployeeWaiter.FirstName} {x.EmployeeWaiter.Patronymic}"))
                .ForMember(x => x.TableNumber, opt => opt.MapFrom(x => x.Table.Number))
                .ForMember(x => x.MenuItem, opt => opt.MapFrom(x => x.MenuItem.Title))
                .ForMember(x => x.LoyaltyCardNumber, opt => opt.MapFrom(x => x.LoyaltyCard != null
                                                                        ? x.LoyaltyCard.Number
                                                                        : null))
                .ForMember(x => x.LoyaltyCardType, opt => opt.MapFrom(x => x.LoyaltyCard != null
                                                                        ? x.LoyaltyCard.LoyaltyCardType.GetDisplayName()
                                                                        : null))
                .ForMember(x => x.OrderCost, opt => opt.MapFrom(x => x.LoyaltyCard != null
                                                                        ? x.MenuItem.Cost * (1 - (((int)(x.LoyaltyCard.LoyaltyCardType)) + 1) * 0.05M)
                                                                        : x.MenuItem.Cost))
                .ForMember(x => x.OrderStatus, opt => opt.MapFrom(x => x.OrderStatus))
                .ForMember(x => x.MenuItem, opt => opt.MapFrom(x => x.MenuItem.Title))
                .ForMember(x => x.EmployeeCashierFIO, opt => opt.MapFrom(x => $"{x.EmployeeCashier!.LastName} {x.EmployeeCashier.FirstName} {x.EmployeeCashier.Patronymic}"))
                ;

        }
    }

}
