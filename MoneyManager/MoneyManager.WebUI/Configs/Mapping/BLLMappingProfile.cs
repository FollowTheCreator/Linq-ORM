using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.Asset;
using MoneyManager.BLL.Interfaces.Models.Category;
using MoneyManager.BLL.Interfaces.Models.Type;
using MoneyManager.BLL.Interfaces.Models.User;

namespace MoneyManager.WebUI.Configs.Mapping
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateUserMaps();

            CreateAssetMaps();

            CreateTypeMaps();

            CreateCategoryMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<DAL.Interfaces.Models.User, User>();
            CreateMap<User, DAL.Interfaces.Models.User>();
            CreateMap<CreateUserModel, DAL.Interfaces.Models.User>()
                .ForMember(
                    dest => dest.Hash,
                    opt => opt.MapFrom(src => src.Password)
                );
            CreateMap<UpdateUserModel, User>()
                .ForMember(
                    dest => dest.Hash,
                    opt => opt.MapFrom(src => src.Password)
                );
        }

        private void CreateAssetMaps()
        {
            CreateMap<Asset, DAL.Interfaces.Models.Asset>();
            CreateMap<DAL.Interfaces.Models.Asset, Asset>();
        }

        private void CreateTypeMaps()
        {
            CreateMap<Type, DAL.Interfaces.Models.Type>();
            CreateMap<DAL.Interfaces.Models.Type, Type>();
        }

        private void CreateCategoryMaps()
        {
            CreateMap<Category, DAL.Interfaces.Models.Category>();
            CreateMap<DAL.Interfaces.Models.Category, Category>();
        }
    }
}
