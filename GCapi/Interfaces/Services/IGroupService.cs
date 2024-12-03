using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces.Services
{
    public interface IGroupService
    {
        Task<IActionResult> AddGroup(GroupDto gr);
        Task<IActionResult> EditGroup(GroupDto gr);
        Task<List<GroupDto>> GetAllGroups();
        Task<List<GroupDto>> GetUserGroups(Guid userId);
        Task<List<UserModel>> GetUsersFromGroup(Guid id);
        Task<IActionResult> RemoveGroup(Guid id);
        public Task<GroupDto?> GetGroup(Guid grId);
        //invite
        Task<string> GetInvite(Guid grId, Guid userId);
        Task<Guid?> CheckInvite(string code, Guid userId); //при возврате null возвращается StatusCode не 200, а 204, поэтому удобнее работать с апи
    }
}
