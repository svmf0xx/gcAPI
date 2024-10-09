using gcapi.Models;

namespace gcapi.Interfaces
{
    public interface IUserService
    {
        public Task RegisterUser(UserModel user);
        public Task UpdateUserData(UserModel user);
        public Task<bool> LogInCheck(string login, string passwd);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
    }
}
