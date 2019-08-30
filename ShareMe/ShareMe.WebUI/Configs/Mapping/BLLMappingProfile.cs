using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.CategoryModels;
using ShareMe.BLL.Interfaces.Models.CommentModels;
using ShareMe.BLL.Interfaces.Models.PostModels;
using ShareMe.BLL.Interfaces.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }

        private void CreatePostMaps()
        {
            CreateMap<DAL.Interfaces.Models.PostModels.PostPreview, PostPreview>();
            CreateMap<DAL.Interfaces.Models.PostModels.PostViewModel, PostViewModel>();
            CreateMap<DAL.Interfaces.Models.PostModels.PopularPost, PopularPost>();
            CreateMap<DAL.Interfaces.Models.PostModels.UserPost, UserPost>();
        }

        private void CreatePostTagMaps()
        {
        }

        private void CreateTagMaps()
        {
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