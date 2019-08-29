using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.Post;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;

            _mapper = mapper;
        }

        public async Task CreateAsync(Post item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetRecordsAsync(PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsPostExistsAsync(Guid id)
        {
            var result = await _postRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task UpdateAsync(Post item)
        {
            throw new NotImplementedException();
        }
    }
}
