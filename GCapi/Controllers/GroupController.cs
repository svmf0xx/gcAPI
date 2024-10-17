﻿using gcapi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using gcapi.Models;
using gcapi.Dto;
namespace gcapi.Controllers
{
    [Route("api/Groups")]
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
        [Route("GetAllUserGroups")]
        public async Task<IEnumerable<GroupModel>> GetAllUserGroups(Guid userId)
        {
            return await _groupService.GetUserGroups(userId);
        }

        [HttpDelete]
        [Route("RemoveGroup")]
        public async Task<IActionResult> RemoveGroup(Guid grId)
        {
            return await _groupService.RemoveGroup(grId);
        }

    }
}
