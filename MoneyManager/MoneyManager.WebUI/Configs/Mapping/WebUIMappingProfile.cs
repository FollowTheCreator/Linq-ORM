using AutoMapper;
using MoneyManager.WebUI.Models.Asset;
using MoneyManager.WebUI.Models.Category;
using MoneyManager.WebUI.Models.QueriesModels;
using MoneyManager.WebUI.Models.Transaction;
using MoneyManager.WebUI.Models.Type;
using MoneyManager.WebUI.Models.User;
using MoneyManager.WebUI.Models.ViewModels;

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

            CreatePageInfoMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<BLL.Interfaces.Models.User.User, ProtectedUserModel>();
            CreateMap<ProtectedUserModel, BLL.Interfaces.Models.User.User>()
                .ForMember(
                    a => a.Hash,
                    opt => opt.Ignore()
                )
                .ForMember(
                    a => a.Salt,
                    opt => opt.Ignore()
                );
            CreateMap<CreateUserModel, BLL.Interfaces.Models.User.CreateUserModel>()
                .ForMember(
                    a => a.Id,
                    opt => opt.Ignore()
                );
            CreateMap<BLL.Interfaces.Models.User.User, UpdateUserModel>()
                .ForMember(
                    a => a.Password,
                    opt => opt.Ignore()
                )
                .ForMember(
                    a => a.ConfirmPassword,
                    opt => opt.Ignore()
                );
            CreateMap<UpdateUserModel, BLL.Interfaces.Models.User.UpdateUserModel>();

            CreateMap<BLL.Interfaces.Models.User.User, User>();

            CreateMap<BLL.Interfaces.Models.User.UserViewModel, UserViewModel>();
            CreateMap<UserViewModel, BLL.Interfaces.Models.User.UserViewModel>();
        }

        private void CreateAssetMaps()
        {
            CreateMap<Asset, BLL.Interfaces.Models.Asset.Asset>()
                .ForMember(
                    a => a.User,
                    opt => opt.Ignore()
                );
            CreateMap<BLL.Interfaces.Models.Asset.Asset, Asset>();

            CreateMap<BLL.Interfaces.Models.Asset.AssetViewModel, AssetViewModel>();
            CreateMap<AssetViewModel, BLL.Interfaces.Models.Asset.AssetViewModel>();
        }

        private void CreateTypeMaps()
        {
            CreateMap<Type, BLL.Interfaces.Models.Type.Type>();
            CreateMap<BLL.Interfaces.Models.Type.Type, Type>();

            CreateMap<BLL.Interfaces.Models.Type.TypeViewModel, TypeViewModel>();
            CreateMap<TypeViewModel, BLL.Interfaces.Models.Type.TypeViewModel>();
        }

        private void CreateCategoryMaps()
        {
            CreateMap<Category, BLL.Interfaces.Models.Category.Category>();
            CreateMap<BLL.Interfaces.Models.Category.Category, Category>();

            CreateMap<BLL.Interfaces.Models.Category.CategoryViewModel, CategoryViewModel>();
            CreateMap<CategoryViewModel, BLL.Interfaces.Models.Category.CategoryViewModel>();
        }

        private void CreateTransactionMaps()
        {
            CreateMap<Transaction, BLL.Interfaces.Models.Transaction.Transaction>();
            CreateMap<BLL.Interfaces.Models.Transaction.Transaction, Transaction>();

            CreateMap<BLL.Interfaces.Models.Transaction.TransactionViewModel, TransactionViewModel>();
            CreateMap<TransactionViewModel, BLL.Interfaces.Models.Transaction.TransactionViewModel>();
        }

        private void CreateQueriesMaps()
        {
            CreateMap<UserIdEmailName, BLL.Interfaces.Models.QueriesModels.UserIdEmailName>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.UserIdEmailName, UserIdEmailName>();

            CreateMap<AmountOfCategories, BLL.Interfaces.Models.QueriesModels.AmountOfCategories>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.AmountOfCategories, AmountOfCategories>();

            CreateMap<TotalAmountForDate, BLL.Interfaces.Models.QueriesModels.TotalAmountForDate>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.TotalAmountForDate, TotalAmountForDate>();

            CreateMap<UserAsset, BLL.Interfaces.Models.QueriesModels.UserAsset>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.UserAsset, UserAsset>();

            CreateMap<UserBalance, BLL.Interfaces.Models.QueriesModels.UserBalance>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.UserBalance, UserBalance>();

            CreateMap<UserTransaction, BLL.Interfaces.Models.QueriesModels.UserTransaction>();
            CreateMap<BLL.Interfaces.Models.QueriesModels.UserTransaction, UserTransaction>();
        }

        private void CreatePageInfoMaps()
        {
            CreateMap<PageInfo, BLL.Interfaces.Models.PageInfo>();
            CreateMap<BLL.Interfaces.Models.PageInfo, PageInfo>();
        }
    }
}
