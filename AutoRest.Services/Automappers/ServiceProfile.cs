using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using AutoRest.Context.Contracts.Enums;
using AutoRest.Context.Contracts.Models;
using AutoRest.Services.Contracts.Models;
using AutoRest.Services.Contracts.Models.Enums;
namespace AutoRest.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<LoyaltyCardType, LoyaltyCardTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<EmployeeTypes, EmployeeTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<MenuItem, MenuItemModel>(MemberList.Destination);

            CreateMap<Person, PersonModel>(MemberList.Destination);

            CreateMap<LoyaltyCard, LoyaltyCardModel>(MemberList.Destination);

            CreateMap<Employee, EmployeeModel>(MemberList.Destination)
                .ForMember(x => x.Person, next => next.Ignore());

            CreateMap<Table, TableModel>(MemberList.Destination);

            CreateMap<OrderItem, OrderItemModel>(MemberList.Destination)
                .ForMember(x => x.EmployeeWaiter, next => next.Ignore())
                .ForMember(x => x.Table, next => next.Ignore())
                .ForMember(x => x.MenuItem, next => next.Ignore())
                .ForMember(x => x.LoyaltyCard, next => next.Ignore())
                .ForMember(x => x.EmployeeCashier, next => next.Ignore());
        }
    }
}
