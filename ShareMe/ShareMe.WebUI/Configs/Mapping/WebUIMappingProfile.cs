using AutoMapper;
using ShareMe.WebUI.Models;
using ShareMe.WebUI.Models.CategoryModels;
using ShareMe.WebUI.Models.CommentModels;
using ShareMe.WebUI.Models.PostModels;
using ShareMe.WebUI.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebUI.Configs.Mapping
{
    public class WebUIMappingProfile : Profile
    {
        public WebUIMappingProfile()
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
            CreateMap<BLL.Interfaces.Models.CategoryModels.CategoryViewModel, CategoryViewModel>();
        }

        private void CreateCommentMaps()
        {
            CreateMap<BLL.Interfaces.Models.CommentModels.CommentViewModel, CommentViewModel>();
        }

        private void CreatePostMaps()
        {
            CreateMap<BLL.Interfaces.Models.PostModels.PostPreview, PostPreview>();
            CreateMap<BLL.Interfaces.Models.PostModels.PostPreviewViewModel, PostPreviewViewModel>();
            CreateMap<BLL.Interfaces.Models.PostModels.PostViewModel, PostViewModel>();
            CreateMap<BLL.Interfaces.Models.PostModels.PopularPost, PopularPost>();
            CreateMap<BLL.Interfaces.Models.PostModels.UserPost, UserPost>();
        }

        private void CreatePostTagMaps()
        {
        }

        private void CreateTagMaps()
        {
        }

        private void CreateUserMaps()
        {
            CreateMap<BLL.Interfaces.Models.UserModels.UserViewModel, UserViewModel>();
        }

        private void CreatePageInfoMaps()
        {
            CreateMap<PageInfo, BLL.Interfaces.Models.PageInfo>();
            CreateMap<BLL.Interfaces.Models.PageInfo, PageInfo>();
        }
    }
}
