using Microsoft.EntityFrameworkCore;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Models.CommentModels;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ShareMeContext context)
            : base(context)
        { }

        public async Task<List<Comment>> GetChildrenAsync(Guid id)
        {
            var result = await DbSet
                .AsNoTracking()
                .Where(comment => comment.ParentId == id)
                .ToListAsync();

            return result;
        }

        public async Task<List<CommentViewModel>> GetPostCommentsAsync(Guid postId)
        {
            var comments = await DbSet
                .Include(comment => comment.User)
                .AsNoTracking()
                .Where(comment =>
                    comment.PostId == postId &&
                    comment.ParentId == null
                )
                .ToListAsync();

            var result = (await Task.WhenAll(comments
                    .Select(async comment => 
                        new CommentViewModel
                        {
                            Id = comment.Id,
                            Content = comment.Content,
                            Date = comment.Date,
                            UserId = comment.User.Id,
                            UserName = comment.User.Name,
                            UserImage = comment.User.Image,
                            Children = await DbSet
                                .Include(childComment => childComment.User)
                                .Where(childComment => childComment.ParentId == comment.Id)
                                .Select(childComment => new CommentViewModel
                                {
                                    Id = childComment.Id,
                                    Content = childComment.Content,
                                    Date = childComment.Date,
                                    UserId = childComment.User.Id,
                                    UserImage = childComment.User.Image,
                                    UserName = childComment.User.Name
                                })
                                .OrderBy(childComment => childComment.Date)
                                .ToListAsync()
                        }
                    )
                ))
                .OrderBy(comment => comment.Date)
                .ToList();

            return result;
        }

        public async Task<int> GetPostCommentsCountAsync(Guid postId)
        {
            var result = await DbSet
                .Where(comment => comment.PostId == postId)
                .CountAsync();

            return result;
        }
    }
}
