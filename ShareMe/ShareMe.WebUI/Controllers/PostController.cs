using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.WebUI.Models;
using ShareMe.WebUI.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<ActionResult> Create(PostCreateModel model)
        {
            model.Header = "Test create";
            model.Content = "Test create";
            model.CategoryId = Guid.Parse("21C59A9A-888C-4C2E-8C5C-8B31AC0C191D");
            model.UserId = Guid.Parse("0DC85AA9-566B-45F2-A92C-4E594D23CFA7");
            model.Tags = new List<string> { "Photography", "Sports", "Life" };

            var convertedModel = _mapper.Map<PostCreateModel, BLL.Interfaces.Models.PostModels.PostCreateModel>(model);

            await _postService.CreateAsync(convertedModel);

            return RedirectToAction("Posts");
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await _postService.DeleteAsync(id);

            return RedirectToAction("Posts");
        }
    }
}