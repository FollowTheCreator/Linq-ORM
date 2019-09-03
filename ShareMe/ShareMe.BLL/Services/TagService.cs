using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.TagModels;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        private readonly IConfigService _configService;
        private readonly IIsEntityExistsService _isEntityExistsService;

        private readonly IMapper _mapper;

        public TagService
        (
            ITagRepository tagRepository, 
            IConfigService configService, 
            IIsEntityExistsService isEntityExistsService, 
            IMapper mapper
        )
        {
            _tagRepository = tagRepository;

            _configService = configService;
            _isEntityExistsService = isEntityExistsService;

            _mapper = mapper;
        }

        public async Task<Tag> GetOrCreateAsync(string name)
        {
            var result = await GetByNameAsync(name);
            if (result == null)
            {
                result = await CreateAsync(name);
            }

            return result;
        }

        public async Task<Tag> CreateAsync(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            var convertedItem = _mapper.Map<Tag, DAL.Interfaces.Models.TagModels.Tag>(tag);
            await _tagRepository.CreateAsync(convertedItem);

            return tag;
        }

        public async Task<List<string>> GetPostTagsAsync(Guid postId)
        {
            if (!await _isEntityExistsService.IsPostExistsAsync(postId))
            {
                throw new ArgumentException(nameof(postId), $"Post with id: {postId} doesn't exist");
            }

            var result = await _tagRepository.GetPostTagsAsync(postId);

            return result;
        }

        public async Task<List<string>> GetTagsAsync(PageInfo pageInfo)
        {
            if (pageInfo == null)
            {
                throw new ArgumentNullException(nameof(pageInfo));
            }

            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);

            var result = await _tagRepository.GetTagsAsync(convertedPageInfo);

            return result;
        }

        public async Task<Tag> GetByNameAsync(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var result = await _tagRepository.GetByNameAsync(name);

            var convertedResult = _mapper.Map<DAL.Interfaces.Models.TagModels.Tag, Tag>(result);

            return convertedResult;
        }
    }
}
