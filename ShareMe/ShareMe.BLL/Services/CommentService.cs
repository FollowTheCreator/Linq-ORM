using AutoMapper;
using ShareMe.BLL.Interfaces.Models.CommentModels;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;

            _mapper = mapper;
        }

        public async Task CreateAsync(Comment item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Date = DateTime.Now;

            var convertedItem = _mapper.Map<Comment, DAL.Interfaces.Models.CommentModels.Comment>(item);

            await _commentRepository.CreateAsync(convertedItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            var children = await _commentRepository.GetChildrenAsync(id);
            foreach(var child in children)
            {
                await _commentRepository.DeleteAsync(child.Id);
            }

            await _commentRepository.DeleteAsync(id);
        }

        public async Task DeleteByPostIdAsync(Guid postId)
        {
            await _commentRepository.DeleteByPostIdAsync(postId);
        }

        public async Task UpdateAsync(Comment item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var convertedItem = _mapper.Map<Comment, DAL.Interfaces.Models.CommentModels.Comment>(item);

            await _commentRepository.UpdateAsync(convertedItem);
        }
    }
}
