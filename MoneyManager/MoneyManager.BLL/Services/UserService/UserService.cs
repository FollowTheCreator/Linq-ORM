using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.User;
using MoneyManager.BLL.Interfaces.Services.UserService;
using MoneyManager.DAL.Interfaces.Repositories.UserRepository;
using Utils;

namespace MoneyManager.BLL.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        private readonly Coder _coder;
        private readonly Generate _generate;

        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper, Coder coder, Generate generate)
        {
            _repository = repository;
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

                await _repository.CreateAsync(convertedItem);

                return result;
            }

            result.AlreadyExists = true;
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            var convertedUsers = _mapper.Map<IEnumerable<DAL.Interfaces.Models.User> , IEnumerable<User>>(users);

            return convertedUsers;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);

            var convertedItem = _mapper.Map<DAL.Interfaces.Models.User, User>(item);

            return convertedItem;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _repository.IsEmailExistsAsync(email);
        }

        public async Task UpdateAsync(UpdateUserModel item)
        {
            if (string.IsNullOrWhiteSpace(item.Password))
            {
                var oldProfile = await _repository.GetByIdAsync(item.Id);
                item.Password = oldProfile.Hash;
            }
            else
            {
                var salt = _generate.RandomSalt();
                item.Password = _coder.Encode(item.Password + salt);
            }

            var user = _mapper.Map<UpdateUserModel, User>(item);
            user.Salt = await _repository.GetSaltByIdAsync(user.Id);

            var convertedUser = _mapper.Map<User, DAL.Interfaces.Models.User>(user);

            await _repository.UpdateAsync(convertedUser);
        }
    }
}
