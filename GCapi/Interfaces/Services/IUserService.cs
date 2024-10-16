using gcapi.Models;
using gcapi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces.Services
{
    public interface IUserService
    {
        Task<IActionResult> EditUser(UpdateUserDto user);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserDto?> GetUserByTgId(long tgid);
        Task<UserDto?> GetUserByUsername(string username);
        Task<IActionResult> RemoveUser(Guid userId);
    }
}
