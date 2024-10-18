using gcapi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using gcapi.Models;
using gcapi.Dto;
namespace gcapi.Controllers
{
    [Route("api/Group")]
    [ApiController]
    public class GroupController(ILogger<UsersController> logger, IGroupService groupService)
    {
        private readonly IGroupService _groupService = groupService;
        private readonly ILogger _logger = logger;

        [HttpGet]
        [Route("GetAllGroups")]
        public async Task<IEnumerable<GroupModel>> GetAllGroups()
        {
            return await _groupService.GetAllGroups();
        }

        [HttpPut]
        [Route("AddGroup")]
        public async Task<IActionResult> AddGroup(GroupDto gr)
        {
            return await _groupService.AddGroup(gr);
        }

        [HttpPut]
        [Route("EditGroup")]
        public async Task<IActionResult> EditGroup(GroupDto gr)
        {
            return await _groupService.EditGroup(gr);
        }

        [HttpGet]
        [Route("GetAllGroupUsers")]
        public async Task<IEnumerable<UserModel>> GetAllGroupUsers(Guid grId)
        {
            return await _groupService.GetUsersFromGroup(grId);
        }

        [HttpGet]
        [Route("GetGroup")]
        public async Task<GroupModel?> GetGroup(Guid grId)
        {
            return await _groupService.GetGroup(grId);
        }

        [HttpGet]
        [Route("GetAllUserGroups")]
        public async Task<IEnumerable<GroupModel>> GetAllUserGroups(Guid userId, bool includeAll = false)
        {
            return await _groupService.GetUserGroups(userId, includeAll);
        }

        [HttpDelete]
        [Route("RemoveGroup")]
        public async Task<IActionResult> RemoveGroup(Guid grId)
        {
            return await _groupService.RemoveGroup(grId);
        }

        [HttpGet]
        [Route("GetInvite")]
        public async Task<string> GetInvite(Guid grId, Guid userId)
        {
            //либо берётся из базы существующий инвайт для данного пользователя, обновлется ExpiredAt
            //либо создается новый
            //ну если вышло время то новый, а если нет то обновляется ExpiredAt
            return await _groupService.GetInvite(grId, userId);
            
        }

        [HttpGet]
        [Route("CheckInvite")]
        public async Task<Guid?> CheckInvite(string invite, Guid userId)
        {
            //Если инвайт существует, пользователь добавляется в группу, пользователю добавляется группа
            //высылается guid группы
            return await _groupService.CheckInvite(invite, userId);
        }
    }
}
