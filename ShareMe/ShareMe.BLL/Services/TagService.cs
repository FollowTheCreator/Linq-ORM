using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.Tag;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task CreateAsync(Tag item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> GetRecordsAsync(PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsTagExistsAsync(Guid id)
        {
            var result = await _tagRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task UpdateAsync(Tag item)
        {
            throw new NotImplementedException();
        }
    }
}
