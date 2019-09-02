using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.WebUI.Models.CommentModels;
using ShareMe.WebUI.Models.PostModels;
using System;
using System.Threading.Tasks;

namespace ShareMe.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IPostService postService, IMapper mapper)
        {
            _commentService = commentService;
            _postService = postService;

            _mapper = mapper;
        }

        public async Task<ActionResult<PostViewModel>> Publish(Comment item)
        {
            var convertedItem = _mapper.Map<Comment, BLL.Interfaces.Models.CommentModels.Comment>(item);
            await _commentService.CreateAsync(convertedItem);

            var post = await _postService.GetPostViewModelAsync(item.PostId);
            var convertedPost = _mapper.Map<BLL.Interfaces.Models.PostModels.PostViewModel, PostViewModel>(post);

            return View("~/Views/Post/Post.cshtml", convertedPost);
        }

        public async Task<ActionResult<PostViewModel>> Delete(Guid commentId, Guid postId)
        {
            await _commentService.DeleteAsync(commentId);

            var post = await _postService.GetPostViewModelAsync(postId);
            var convertedPost = _mapper.Map<BLL.Interfaces.Models.PostModels.PostViewModel, PostViewModel>(post);

            return View("~/Views/Post/Post.cshtml", convertedPost);
        }
    }
}