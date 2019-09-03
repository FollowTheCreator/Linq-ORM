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
        private readonly IIsEntityExistsService _isEntityExistsService;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPostService postService, IIsEntityExistsService isEntityExistsService, IMapper mapper)
        {
            _userRepository = userRepository;

            _postService = postService;
            _isEntityExistsService = isEntityExistsService;

            _mapper = mapper;
        }

        public async Task CreateAsync(User item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var convertedItem = _mapper.Map<User, DAL.Interfaces.Models.UserModels.User>(item);

            await _userRepository.CreateAsync(convertedItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<UserViewModel> GetUserAsync(Guid id)
        {
            if (!await _isEntityExistsService.IsUserExistsAsync(id))
            {
                throw new ArgumentException(nameof(id), "User with this Id doesn't exist");
            }

            var user = await _userRepository.GetUserAsync(id);
            var userView = _mapper.Map<DAL.Interfaces.Models.UserModels.User, UserViewModel>(user);

            var posts = await _postService.GetUserPostsAsync(userView.Id);
            userView.UserPosts = posts;

            return userView;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await _userRepository.IsEmailExistsAsync(email);
        }

        public async Task UpdateAsync(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var convertedItem = _mapper.Map<User, DAL.Interfaces.Models.UserModels.User>(item);

            await _userRepository.UpdateAsync(convertedItem);
        }
    }
}
