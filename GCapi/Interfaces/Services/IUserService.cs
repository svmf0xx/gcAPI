using gcapi.Models;
using gcapi.Dto;

namespace gcapi.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        Task<UserDto?> GetUserByTgId(long tgid);
        Task<UserDto?> GetUserByUsername(string username);
        Task<bool> EditUser(UpdateUserDto user);
        Task RemoveUser(Guid userId);
    }
}
