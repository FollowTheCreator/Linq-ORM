using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.WebUI.Models;
using ShareMe.WebUI.Models.PostModels;

namespace ShareMe.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;

            _mapper = mapper;
        }

        public async Task<ActionResult<PostPreviewViewModel>> Posts(PageInfo pageInfo)
        {
            var convertedPageInfo = _mapper.Map<PageInfo, BLL.Interfaces.Models.PageInfo>(pageInfo);
            var result = await _postService.GetPostPreviewsAsync(convertedPageInfo);
            var convertedResult = _mapper.Map<BLL.Interfaces.Models.PostModels.PostPreviewViewModel, PostPreviewViewModel>(result);

            return View("~/Views/Post/Posts.cshtml", convertedResult);
        }

        public async Task<ActionResult<PostPreviewViewModel>> CategoryPosts(PageInfo pageInfo, Guid categoryId)
        {
            var convertedPageInfo = _mapper.Map<PageInfo, BLL.Interfaces.Models.PageInfo>(pageInfo);
            var result = await _postService.GetPostPreviewsByCategoryAsync(convertedPageInfo, categoryId);
            var convertedResult = _mapper.Map<BLL.Interfaces.Models.PostModels.PostPreviewViewModel, PostPreviewViewModel>(result);

            return View("~/Views/Post/Posts.cshtml", convertedResult);
        }

        public async Task<ActionResult<PostPreviewViewModel>> TagPosts(PageInfo pageInfo, string tag)
        {
            var convertedPageInfo = _mapper.Map<PageInfo, BLL.Interfaces.Models.PageInfo>(pageInfo);
            var result = await _postService.GetPostPreviewsByTagAsync(convertedPageInfo, tag);
            var convertedResult = _mapper.Map<BLL.Interfaces.Models.PostModels.PostPreviewViewModel, PostPreviewViewModel>(result);

            return View("~/Views/Post/Posts.cshtml", convertedResult);
        }

        public async Task<ActionResult<PostPreviewViewModel>> SearchPosts(PageInfo pageInfo, string header)
        {
            var convertedPageInfo = _mapper.Map<PageInfo, BLL.Interfaces.Models.PageInfo>(pageInfo);
            var result = await _postService.GetPostPreviewsBySearchAsync(convertedPageInfo, header);
            var convertedResult = _mapper.Map<BLL.Interfaces.Models.PostModels.PostPreviewViewModel, PostPreviewViewModel>(result);

            return View("~/Views/Post/Posts.cshtml", convertedResult);
        }

        public async Task<ActionResult<PostViewModel>> Post(Guid id)
        {
            var result = await _postService.GetPostViewModelAsync(id);
            var convertedResult = _mapper.Map<BLL.Interfaces.Models.PostModels.PostViewModel, PostViewModel>(result);

            return View("~/Views/Post/Post.cshtml", convertedResult);
        }
    }
}