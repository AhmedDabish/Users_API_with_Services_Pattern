using UsersApiExample.DTOs;
using UsersApiExample.Models;
using UsersApiExample.Repositories;

namespace UsersApiExample.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto { Id = u.Id, Name = u.Name, Email = u.Email });
        }

        public async Task<UserDto> CreateUserAsync(string name, string email)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name cannot be empty");

            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            var user = new User { Name = name, Email = email };
            await _userRepository.AddAsync(user);

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }
    }
}
