using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.Comment;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comment>> GetRecordsAsync(PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsCommentExistsAsync(Guid id)
        {
            var result = await _commentRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task UpdateAsync(Comment item)
        {
            throw new NotImplementedException();
        }
    }
}
