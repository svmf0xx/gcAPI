using gcapi.DataBaseModels;
using gcapi.Dto;
using gcapi.Models;

namespace gcapi.Interfaces
{
    public interface IGroupService
    {
        Task AddGroud(GroupDto gr);
        Task<bool> EditGroud(GroupDto gr);
        Task<List<GroupModel>> GetAllGroups();
        Task<List<GroupModel>> GetUserGroupsByLogin(string login);
        Task<List<UserModel>> GetUsersFromGroup(Guid id);
        Task<bool> RemoveGroud(GroupDto gr);
    }
}
