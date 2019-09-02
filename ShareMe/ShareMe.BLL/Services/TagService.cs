using AutoMapper;
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

        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;

            _mapper = mapper;
        }

        public async Task<Tag> CreateIfExistsAsync(string name)
        {
            var result = await GetByNameAsync(name);
            if (result == null)
            {
                var item = new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };
                var convertedItem = _mapper.Map<Tag, DAL.Interfaces.Models.TagModels.Tag>(item);
                await _tagRepository.CreateAsync(convertedItem);

                result = item;
            }

            return result;
        }

        public async Task<List<string>> GetPostTagsAsync(Guid postId)
        {
            var result = await _tagRepository.GetPostTagsAsync(postId);

            return result;
        }

        public async Task<bool> IsTagExistsAsync(Guid id)
        {
            var result = await _tagRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task<List<string>> GetTagsAsync()
        {
            var result = await _tagRepository.GetTagsAsync();

            return result;
        }

        public async Task<Tag> GetByNameAsync(string name)
        {
            var result = await _tagRepository.GetByNameAsync(name);

            var convertedResult = _mapper.Map<DAL.Interfaces.Models.TagModels.Tag, Tag>(result);

            return convertedResult;
        }
    }
}
