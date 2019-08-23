using AutoMapper;
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

        private void CreateTransactionMaps()
        {
            CreateMap<Transaction, DAL.Interfaces.Models.Transaction>();
            CreateMap<DAL.Interfaces.Models.Transaction, Transaction>();
        }

        private void CreateQueriesMaps()
        {
            CreateMap<UserIdEmailName, DAL.Interfaces.Models.QueriesModels.UserIdEmailName>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.UserIdEmailName, UserIdEmailName>();

            CreateMap<AmountOfParents, DAL.Interfaces.Models.QueriesModels.AmountOfParents>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.AmountOfParents, AmountOfParents>();

            CreateMap<TotalAmountForDate, DAL.Interfaces.Models.QueriesModels.TotalAmountForDate>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.TotalAmountForDate, TotalAmountForDate>();

            CreateMap<UserAsset, DAL.Interfaces.Models.QueriesModels.UserAsset>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.UserAsset, UserAsset>();

            CreateMap<UserBalance, DAL.Interfaces.Models.QueriesModels.UserBalance>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.UserBalance, UserBalance>();

            CreateMap<UserTransaction, DAL.Interfaces.Models.QueriesModels.UserTransaction>();
            CreateMap<DAL.Interfaces.Models.QueriesModels.UserTransaction, UserTransaction>();
        }
    }
}
