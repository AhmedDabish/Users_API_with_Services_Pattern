using UsersApiExample.DTOs;

namespace UsersApiExample.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(string name, string email);
        Task<bool> DeleteUserAsync(int id); 
    }
}
