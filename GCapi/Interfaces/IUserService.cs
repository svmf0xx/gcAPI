using gcapi.Models;

namespace gcapi.Interfaces
{
    public interface IUserService
    {
        public Task RegisterUser(UserModel user);
        public Task UpdateUserData(UserModel user);
        public Task<bool> LogIn(string login, string passwd);
    }
}
