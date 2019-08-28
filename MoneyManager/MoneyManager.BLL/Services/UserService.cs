using AutoMapper;
using MoneyManager.BLL.Interfaces.Models;
using MoneyManager.BLL.Interfaces.Models.User;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace MoneyManager.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IConfigService _configService;

        private readonly Coder _coder;
        private readonly Generate _generate;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, Coder coder, Generate generate, IConfigService configService)
        {
            _userRepository = userRepository;

            _configService = configService;

            _coder = coder;
            _generate = generate;

            _mapper = mapper;
        }

        public async Task<CreateUserResult> CreateAsync(CreateUserModel item)
        {
            var result = new CreateUserResult();

            if(!await IsEmailExistsAsync(item.Email))
            {
                result.AlreadyExists = false;

                item.Id = Guid.NewGuid();
                var convertedItem = _mapper.Map<CreateUserModel, DAL.Interfaces.Models.User>(item);
                convertedItem.Salt = _generate.RandomSalt();
                convertedItem.Hash = _coder.Encode(convertedItem.Hash + convertedItem.Salt);

                await _userRepository.CreateAsync(convertedItem);

                return result;
            }

            result.AlreadyExists = true;
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<UserViewModel> GetRecordsAsync(PageInfo pageInfo)
        {
            pageInfo.CheckPageInfo(_configService.GetPageSize());

            var convertedPageInfo = _mapper.Map<PageInfo, DAL.Interfaces.Models.PageInfo>(pageInfo);
            var users = await _userRepository.GetRecordsAsync(convertedPageInfo);

            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.User> , IEnumerable<User>>(users);

            pageInfo.TotalItems = await _userRepository.RecordsCountAsync();
            pageInfo.TotalPages = (int)Math.Ceiling(pageInfo.TotalItems / (double)pageInfo.PageSize);

            return new UserViewModel
            {
                Users = convertedItems,
                PageInfo = pageInfo
            };
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var item = await _userRepository.GetByIdAsync(id);

            var convertedItem = _mapper.Map<DAL.Interfaces.Models.User, User>(item);

            return convertedItem;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _userRepository.IsEmailExistsAsync(email);
        }

        public async Task<UpdateUserResult> UpdateAsync(UpdateUserModel item)
        {
            var result = new UpdateUserResult
            {
                IsUserExists = await IsUserExistsAsync(item.Id)
            };

            if (result.IsUserExists)
            {
                var user = _mapper.Map<UpdateUserModel, User>(item);

                if (string.IsNullOrWhiteSpace(user.Hash))
                {
                    var oldProfile = await _userRepository.GetByIdAsync(item.Id);
                    user.Hash = oldProfile.Hash;
                }
                else
                {
                    var salt = _generate.RandomSalt();
                    user.Hash = _coder.Encode(user.Hash + salt);
                }

                user.Salt = await GetSaltByIdAsync(user.Id);

                var convertedUser = _mapper.Map<User, DAL.Interfaces.Models.User>(user);

                await _userRepository.UpdateAsync(convertedUser);
            }

            return result;
        }

        public async Task<string> GetSaltByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return user.Salt;
        }

        public async Task<bool> IsUserExistsAsync(Guid id)
        {
            var result = await _userRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}
