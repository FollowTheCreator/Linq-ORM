using AutoMapper;
using ShareMe.BLL.Interfaces.Models;
using ShareMe.BLL.Interfaces.Models.User;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareMe.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;

            _mapper = mapper;
        }

        public async Task CreateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetRecordsAsync(PageInfo pageInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetSaltByIdAsync(Guid id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
