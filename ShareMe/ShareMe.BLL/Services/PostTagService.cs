using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.PostTagModels;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class PostTagService : IPostTagService
    {
        private readonly IPostTagRepository _postTagRepository;

        private readonly IMapper _mapper;

        public PostTagService(IPostTagRepository postTagRepository, IMapper mapper)
        {
            _postTagRepository = postTagRepository;

            _mapper = mapper;
        }

        public async Task CreateAsync(PostTag item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PostTag> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostTag>> GetRecordsAsync(PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsPostTagExistsAsync(Guid id)
        {
            var result = await _postTagRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task UpdateAsync(PostTag item)
        {
            throw new NotImplementedException();
        }
    }
}
