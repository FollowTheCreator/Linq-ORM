using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.Asset;
using MoneyManager.BLL.Interfaces.Models.User;

namespace MoneyManager.WebUI.Configs.Mapping
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateUserMaps();

            CreateAssetMaps();
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
    }
}
