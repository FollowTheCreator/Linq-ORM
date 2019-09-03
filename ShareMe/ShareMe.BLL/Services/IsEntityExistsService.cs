using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class IsEntityExistsService : IIsEntityExistsService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public IsEntityExistsService(IPostRepository postRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> IsCategoryExistsAsync(Guid id)
        {
            var result = await _categoryRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task<bool> IsPostExistsAsync(Guid id)
        {
            var result = await _postRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task<bool> IsUserExistsAsync(Guid id)
        {
            var result = await _userRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}
