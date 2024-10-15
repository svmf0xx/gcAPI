using gcapi.DataBaseModels;
using gcapi.Dto;
using gcapi.Models;

namespace gcapi.Interfaces.Services
{
    public interface IGroupService
    {
        Task AddGroup(GroupDto gr);
        Task<bool> EditGroup(GroupDto gr);
        Task<List<GroupModel>> GetAllGroups();
        Task<List<GroupModel>> GetUserGroups(string username);
        Task<List<UserModel>> GetUsersFromGroup(Guid id);
        Task<bool> RemoveGroup(Guid id);
    }
}
