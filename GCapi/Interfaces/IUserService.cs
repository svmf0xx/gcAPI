using gcapi.Dto;
using gcapi.Models;

namespace gcapi.Interfaces
{
    public interface IUserService
    {
        public Task RegisterUser(RegisterDto user);
        public Task<bool> LogInCheck(string login, string passwd);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<bool> EditUser(UpdateUserDto user);
        Task RemoveUser(UpdateUserDto user);
    }
}
