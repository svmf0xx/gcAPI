using gcapi.DataBaseModels;

namespace gcapi.Interfaces
{
    public interface IAuthService
    {
        public Task AuthorizeUser();
        public Task<bool> RegisterUser(UserModel user);
    }
}
