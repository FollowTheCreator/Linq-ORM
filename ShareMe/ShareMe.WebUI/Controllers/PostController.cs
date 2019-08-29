using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models;

namespace ShareMe.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly ShareMeContext _context;

        public PostController(ShareMeContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Post>>> Posts()
        {
            return View("~/Views/Post/Posts.cshtml", await _context.Post.ToListAsync());
        } 
    }
}