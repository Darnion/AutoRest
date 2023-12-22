//using AutoMapper;
//using AutoMapper.Extensions.EnumMapping;
//using AutoRest.Api.Models;
//using AutoRest.Api.Models.Enums;
//using AutoRest.Api.ModelsRequest.MenuItem;
//using AutoRest.Api.ModelsRequest.LoyaltyCard;
//using AutoRest.Api.ModelsRequest.Employee;
//using AutoRest.Api.ModelsRequest.Table;
//using AutoRest.Api.ModelsRequest.Person;
//using AutoRest.Api.ModelsRequest.OrderItem;
//using AutoRest.Services.Contracts.Models;
//using AutoRest.Services.Contracts.Models.Enums;
//using AutoRest.Services.Contracts.ModelsRequest;

//namespace AutoRest.Api.Infrastructures
//{
//    /// <summary>
//    /// Профиль маппера АПИшки
//    /// </summary>
//    public class ApiAutoMapperProfile : Profile
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр <see cref="ApiAutoMapperProfile"/>
//        /// </summary>
//        public ApiAutoMapperProfile()
//        {
//            CreateMap<LoyaltyCardTypesModel, LoyaltyCardTypesResponse>()
//                .ConvertUsingEnumMapping(opt => opt.MapByName())
//                .ReverseMap();
//            CreateMap<EmployeeTypesModel, EmployeeTypesResponse>()
//                .ConvertUsingEnumMapping(opt => opt.MapByName())
//                .ReverseMap();

//            CreateMap<MenuItemModel, MenuItemResponse>(MemberList.Destination);
//            CreateMap<MenuItemRequest, MenuItemModel>(MemberList.Destination);



//            CreateMap<CreateLoyaltyCardRequest, LoyaltyCardRequestModel>(MemberList.Destination);
//            CreateMap<LoyaltyCardRequest, LoyaltyCardRequestModel>(MemberList.Destination);

//            CreateMap<EmployeeModel, EmployeeResponse>(MemberList.Destination)
//                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Person != null
//                    ? $"{x.Person.LastName} {x.Person.FirstName} {x.Person.Patronymic}"
//                    : string.Empty))
//                .ForMember(x => x.EmployeeType, opt => opt.MapFrom(x => x.EmployeeType));

//            CreateMap<CreateEmployeeRequest, EmployeeRequestModel>(MemberList.Destination);
//            CreateMap<EmployeeRequest, EmployeeRequestModel>(MemberList.Destination);

//            CreateMap<PersonModel, PersonResponse>(MemberList.Destination);
//            CreateMap<CreatePersonRequest, PersonRequestModel>(MemberList.Destination);
//            CreateMap<PersonRequest, PersonRequestModel>(MemberList.Destination);

//            CreateMap<TableModel, TableResponse>(MemberList.Destination);
//            CreateMap<CreateTableRequest, TableRequestModel>(MemberList.Destination);
//            CreateMap<TableRequest, TableRequestModel>(MemberList.Destination);

//            CreateMap<OrderItemModel, OrderItemResponse>(MemberList.Destination)
//                .ForMember(x => x.NameMenuItem, opt => opt.MapFrom(x => x.MenuItem!.Name))
//                .ForMember(x => x.NameTable, opt => opt.MapFrom(x => x.Table!.Name))
//                .ForMember(x => x.TeacherName, opt => opt.MapFrom(x => $"{x.Teacher!.LastName} {x.Teacher.FirstName} {x.Teacher.Patronymic}"))
//                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Teacher!.Phone));
//            CreateMap<CreateOrderItemRequest, OrderItemRequestModel>(MemberList.Destination);
//            CreateMap<OrderItemRequest, OrderItemRequestModel>(MemberList.Destination);


//        }
//    }

//}
