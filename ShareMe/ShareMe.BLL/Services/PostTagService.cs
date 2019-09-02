using AutoMapper;
using ShareMe.BLL.Interfaces.Models.PostTagModels;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
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
            var convertedItem = _mapper.Map<PostTag, DAL.Interfaces.Models.PostTagModels.PostTag>(item);
            await _postTagRepository.CreateAsync(convertedItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _postTagRepository.DeleteAsync(id);
        }

        public async Task<List<PostTag>> GetPostTagsByPostId(Guid postId)
        {
            var result = await _postTagRepository.GetPostTagsByPostId(postId);

            var convertedResult = _mapper.Map<List<DAL.Interfaces.Models.PostTagModels.PostTag>, List<PostTag>>(result);

            return convertedResult;
        }

        public async Task<bool> IsPostTagExistsAsync(Guid id)
        {
            var result = await _postTagRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}
