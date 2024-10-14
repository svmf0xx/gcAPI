using gcapi.DataBaseModels;
using gcapi.Dto;

namespace gcapi.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<bool> EditUser(UpdateUserDto user);
        Task RemoveUser(Guid userId);
    }
}
