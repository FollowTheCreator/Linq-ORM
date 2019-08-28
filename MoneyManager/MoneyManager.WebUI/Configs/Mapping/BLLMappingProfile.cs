using AutoMapper;
using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.Asset;
using MoneyManager.BLL.Interfaces.Models.Category;
using MoneyManager.BLL.Interfaces.Models.QueriesModels;
using MoneyManager.BLL.Interfaces.Models.Transaction;
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

            CreateTransactionMaps();

            CreateQueriesMaps();

            CreatePageInfoMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<DAL.Interfaces.Models.User, User>();
            CreateMap<User, DAL.Interfaces.Models.User>()
                .ForMember(
                    a => a.Asset,
                    opt => opt.Ignore()
                );
            CreateMap<CreateUserModel, DAL.Interfaces.Models.User>()
                .ForMember(
                    a => a.Asset,
                    opt => opt.Ignore()
                )
                .ForMember(
                    a => a.Hash,
                    opt => opt.Ignore()
                )
                .ForMember(
                    a => a.Salt,
                    opt => opt.Ignore()
                );
            CreateMap<UpdateUserModel, User>()
                .ForMember(
                    a => a.Hash,
                    opt => opt.Ignore()
                )
                .ForMember(
                    a => a.Salt,
                    opt => opt.Ignore()
                );
        }

        private void CreateAssetMaps()
        {
            CreateMap<Asset, DAL.Interfaces.Models.Asset>()
                .ForMember(
                    a => a.Transaction,
                    opt => opt.Ignore()
                );
            CreateMap<DAL.Interfaces.Models.Asset, Asset>();
        }

        private void CreateTypeMaps()
        {
            CreateMap<Type, DAL.Interfaces.Models.Type>()
                .ForMember(
                    a => a.Category,
                    opt => opt.Ignore()
                );
            CreateMap<DAL.Interfaces.Models.Type, Type>();
        }

        private void CreateCategoryMaps()
        {
            CreateMap<Category, DAL.Interfaces.Models.Category>()
                .ForMember(
                    a => a.TypeNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(
                    a => a.Transaction,
                    opt => opt.Ignore()
                );
            CreateMap<DAL.Interfaces.Models.Category, Category>();
        }

        private void CreateTransactionMaps()
        {
            CreateMap<Transaction, DAL.Interfaces.Models.Transaction>()
                .ForMember(
                    a => a.Asset,
                    opt => opt.Ignore()
                )
                .ForMember(
                    a => a.Category,
                    opt => opt.Ignore()
                );
            CreateMap<DAL.Interfaces.Models.Transaction, Transaction>();
        }

        private void CreateQueriesMaps()
        {
            CreateMap<UserIdEmailName, DAL.Interfaces.Models.QueriesModels.UserIdEmailName>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.UserIdEmailName, UserIdEmailName>();

            CreateMap<AmountOfCategories, DAL.Interfaces.Models.QueriesModels.AmountOfCategories>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.AmountOfCategories, AmountOfCategories>();

            CreateMap<TotalAmountForDate, DAL.Interfaces.Models.QueriesModels.TotalAmountForDate>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.TotalAmountForDate, TotalAmountForDate>();

            CreateMap<UserAsset, DAL.Interfaces.Models.QueriesModels.UserAsset>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.UserAsset, UserAsset>();

            CreateMap<UserBalance, DAL.Interfaces.Models.QueriesModels.UserBalance>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.UserBalance, UserBalance>();

            CreateMap<UserTransaction, DAL.Interfaces.Models.QueriesModels.UserTransaction>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.UserTransaction, UserTransaction>();
        }

        private void CreatePageInfoMaps()
        {
            CreateMap<PageInfo, DAL.Interfaces.Models.PageInfo>();
            CreateMap<DAL.Interfaces.Models.PageInfo, PageInfo>();
        }
    }
}
