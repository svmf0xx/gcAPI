using gcapi.DataBaseModels;
using gcapi.Dto;
using gcapi.Models;

namespace gcapi.Interfaces.Services
{
    public interface IGroupService
    {
        Task AddGroud(GroupDto gr);
        Task<bool> EditGroud(GroupDto gr);
        Task<List<GroupModel>> GetAllGroups();
        Task<List<GroupModel>> GetUserGroups(string username);
        Task<List<UserModel>> GetUsersFromGroup(Guid id);
        Task<bool> RemoveGroup(GroupDto gr);
    }
}
