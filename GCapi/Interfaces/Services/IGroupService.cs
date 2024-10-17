using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces.Services
{
    public interface IGroupService
    {
        Task<IActionResult> AddGroup(GroupDto gr);
        Task<IActionResult> EditGroup(GroupDto gr);
        Task<List<GroupModel>> GetAllGroups();
        Task<List<GroupModel>> GetUserGroups(Guid userId);
        Task<List<UserModel>> GetUsersFromGroup(Guid id);
        Task<IActionResult> RemoveGroup(Guid id);

        //invite
        Task<string> GetInvite(Guid grId, Guid userId);
        Task<Guid> CheckInvite(string code, Guid userId);
    }
}
