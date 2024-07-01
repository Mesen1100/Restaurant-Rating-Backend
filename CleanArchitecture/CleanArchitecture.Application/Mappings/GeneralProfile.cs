using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Categories.Queries.GetAllCategories;
using CleanArchitecture.Core.Features.FoodTypes.Queries.GetAllFoodTypes;
using CleanArchitecture.Core.Features.Menus.Queries.GetAllMenus;
using CleanArchitecture.Core.Features.MenuTypes.Queries.GetAllMenuTypes;
using CleanArchitecture.Core.Features.PlaceTypes.Queries.GetAllPlaceTypes;
using CleanArchitecture.Core.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Core.Features.Products.Queries.GetAllProducts;
using CleanArchitecture.Core.Features.Users.Queries.GetAllUsers;

namespace CleanArchitecture.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
            CreateMap<GetAllCategoriesQuery, GetAllCategoriesParameter>();
            CreateMap<Category, GetAllCategoriesViewModel>().ReverseMap();
            CreateMap<User,GetAllUsersViewModel>().ReverseMap();
            CreateMap<GetAllUsersQuery, GetAllUsersParameter>();
            CreateMap<PlaceType,GetAllPlaceTypesViewModel>().ReverseMap();
            CreateMap<Place, GetAllPlaceTypesViewModel>().ReverseMap();
            CreateMap<MenuType, GetAllMenuTypesViewModel>().ReverseMap();
            CreateMap<Menu, GetAllMenusViewModel>().ReverseMap();
            CreateMap<FoodType, GetAllFoodTypesViewModel>().ReverseMap();


        }
    }
}
