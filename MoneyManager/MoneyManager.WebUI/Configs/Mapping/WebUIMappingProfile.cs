using AutoMapper;
using MoneyManager.WebUI.Models.Asset;
using MoneyManager.WebUI.Models.Category;
using MoneyManager.WebUI.Models.QueriesModels;
using MoneyManager.WebUI.Models.Transaction;
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

            CreateTransactionMaps();

            CreateQueriesMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<BLL.Interfaces.Models.User.User, UserViewModel>();
            CreateMap<CreateUserModel, BLL.Interfaces.Models.User.CreateUserModel>();
            CreateMap<BLL.Interfaces.Models.User.User, UpdateUserModel>();
            CreateMap<UpdateUserModel, BLL.Interfaces.Models.User.UpdateUserModel>();

            CreateMap<BLL.Interfaces.Models.User.User, User>();
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

        private void CreateTransactionMaps()
        {
            CreateMap<Transaction, BLL.Interfaces.Models.Transaction.Transaction>();
            CreateMap<BLL.Interfaces.Models.Transaction.Transaction, Transaction>();
        }

        private void CreateQueriesMaps()
        {
            CreateMap<UserIdEmailName, BLL.Interfaces.Models.QueriesModels.UserIdEmailName>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.UserIdEmailName, UserIdEmailName>();

            CreateMap<AmountOfParents, BLL.Interfaces.Models.QueriesModels.AmountOfParents>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.AmountOfParents, AmountOfParents>();

            CreateMap<TotalAmountForDate, BLL.Interfaces.Models.QueriesModels.TotalAmountForDate>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.TotalAmountForDate, TotalAmountForDate>();

            CreateMap<UserAsset, BLL.Interfaces.Models.QueriesModels.UserAsset>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.UserAsset, UserAsset>();

            CreateMap<UserBalance, BLL.Interfaces.Models.QueriesModels.UserBalance>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.UserBalance, UserBalance>();

            CreateMap<UserTransaction, BLL.Interfaces.Models.QueriesModels.UserTransaction>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.UserTransaction, UserTransaction>();
        }
    }
}
