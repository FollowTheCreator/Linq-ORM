using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.CategoryModels;
using ShareMe.BLL.Interfaces.Models.CommentModels;
using ShareMe.BLL.Interfaces.Models.PostModels;
using ShareMe.BLL.Interfaces.Models.PostTagModels;
using ShareMe.BLL.Interfaces.Models.TagModels;
using ShareMe.BLL.Interfaces.Models.UserModels;

namespace ShareMe.WebUI.Configs.Mapping
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateCategoryMaps();

            CreateCommentMaps();

            CreatePostMaps();

            CreatePostTagMaps();

            CreateTagMaps();

            CreateUserMaps();

            CreatePageInfoMaps();
        }

        private void CreateCategoryMaps()
        {
            CreateMap<DAL.Interfaces.Models.CategoryModels.CategoryViewModel, CategoryViewModel>();
        }

        private void CreateCommentMaps()
        {
            CreateMap<DAL.Interfaces.Models.CommentModels.CommentViewModel, CommentViewModel>();
            CreateMap<Comment, DAL.Interfaces.Models.CommentModels.Comment>()
                .ForMember(
                    m => m.Parent,
                    opt => opt.Ignore()
                )
                .ForMember(
                    m => m.Post,
                    opt => opt.Ignore()
                )
                .ForMember(
                    m => m.User,
                    opt => opt.Ignore()
                )
                .ForMember(
                    m => m.InverseParent,
                    opt => opt.Ignore()
                );
        }

        private void CreatePostMaps()
        {
            CreateMap<DAL.Interfaces.Models.PostModels.PostPreview, PostPreview>();
            CreateMap<DAL.Interfaces.Models.PostModels.PostViewModel, PostViewModel>();
            CreateMap<DAL.Interfaces.Models.PostModels.PopularPost, PopularPost>();
            CreateMap<DAL.Interfaces.Models.PostModels.UserPost, UserPost>();
            CreateMap<PostCreateModel, DAL.Interfaces.Models.PostModels.Post>()
                .ForMember(
                    m => m.Category,
                    opt => opt.Ignore()
                )
                .ForMember(
                    m => m.User,
                    opt => opt.Ignore()
                )
                .ForMember(
                    m => m.Comment,
                    opt => opt.Ignore()
                )
                .ForMember(
                    m => m.PostTag,
                    opt => opt.Ignore()
                );
        }

        private void CreatePostTagMaps()
        {
            CreateMap<PostTag, DAL.Interfaces.Models.PostTagModels.PostTag>()
                .ForMember(
                    m => m.Post,
                    opt => opt.Ignore()
                )
                .ForMember(
                    m => m.Tag,
                    opt => opt.Ignore()
                );

            CreateMap<DAL.Interfaces.Models.PostTagModels.PostTag, PostTag>();
        }

        private void CreateTagMaps()
        {
            CreateMap<Tag, DAL.Interfaces.Models.TagModels.Tag>()
                .ForMember(
                    m => m.PostTag,
                    opt => opt.Ignore()
                );
            CreateMap<DAL.Interfaces.Models.TagModels.Tag, Tag>();
        }

        private void CreateUserMaps()
        {
            CreateMap<DAL.Interfaces.Models.UserModels.UserViewModel, UserViewModel>();
        }

        private void CreatePageInfoMaps()
        {
            CreateMap<PageInfo, DAL.Interfaces.Models.PageInfo>();
            CreateMap<DAL.Interfaces.Models.PageInfo, PageInfo>();
        }
    }
}