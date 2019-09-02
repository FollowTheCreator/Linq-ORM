using AutoMapper;
using ShareMe.BLL.Interfaces.Models.UserModels;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IPostService _postService;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPostService postService, IMapper mapper)
        {
            _userRepository = userRepository;

            _postService = postService;

            _mapper = mapper;
        }

        public async Task CreateAsync(User item)
        {
            var convertedItem = _mapper.Map<User, DAL.Interfaces.Models.UserModels.User>(item);

            await _userRepository.CreateAsync(convertedItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<UserViewModel> GetUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserAsync(id);
            var convertedUser = _mapper.Map<DAL.Interfaces.Models.UserModels.UserViewModel, UserViewModel>(user);

            var posts = await _postService.GetUserPostsAsync(convertedUser.Id);
            convertedUser.UserPosts = posts;

            return convertedUser;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _userRepository.IsEmailExistsAsync(email);
        }

        public async Task<bool> IsUserExistsAsync(Guid id)
        {
            var result = await _userRepository.GetByIdAsync(id);

            return result != null;
        }

        public async Task UpdateAsync(User item)
        {
            var convertedItem = _mapper.Map<User, DAL.Interfaces.Models.UserModels.User>(item);

            await _userRepository.UpdateAsync(convertedItem);
        }
    }
}
