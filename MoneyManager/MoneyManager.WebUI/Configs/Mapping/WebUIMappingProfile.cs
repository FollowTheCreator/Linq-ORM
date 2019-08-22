using AutoMapper;
using MoneyManager.WebUI.Models.Asset;
using MoneyManager.WebUI.Models.Category;
using MoneyManager.WebUI.Models.Type;
using MoneyManager.WebUI.Models.User;

namespace MoneyManager.WebUI.Configs.Mapping
{
    public class WebUIMappingProfile : Profile
    {
        public WebUIMappingProfile()
        {
            CreateUserMaps();

            CreateAssetMaps();

            CreateTypeMaps();

            CreateCategoryMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<BLL.Interfaces.Models.User.User, UserViewModel>();
            CreateMap<CreateUserModel, BLL.Interfaces.Models.User.CreateUserModel>();
            CreateMap<BLL.Interfaces.Models.User.User, UpdateUserModel>();
            CreateMap<UpdateUserModel, BLL.Interfaces.Models.User.UpdateUserModel>();
        }

        private void CreateAssetMaps()
        {
            CreateMap<Asset, BLL.Interfaces.Models.Asset.Asset>();
            CreateMap<BLL.Interfaces.Models.Asset.Asset, Asset>();
        }

        private void CreateTypeMaps()
        {
            CreateMap<Type, BLL.Interfaces.Models.Type.Type>();
            CreateMap<BLL.Interfaces.Models.Type.Type, Type>();
        }

        private void CreateCategoryMaps()
        {
            CreateMap<Category, BLL.Interfaces.Models.Category.Category>();
            CreateMap<BLL.Interfaces.Models.Category.Category, Category>();
        }
    }
}
