using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gcapi.Models;
using gcapi.DataBase;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Specialized;
using Microsoft.AspNetCore.SignalR;
using gcapi.Interfaces;
using gcapi.Dto;

namespace gcapi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IEventService _eventRepository;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public ApiController(IEventService eventRepository, ILogger<ApiController> logger, IUserService userService, IGroupService groupService)
        {
            _eventRepository = eventRepository;
            _userService = userService;
            _logger = logger;
            _groupService = groupService;
        }

        [HttpGet]
        [Route("GetAllEvents")]
        public async Task<IEnumerable<EventModel>> GetEvents()
        {
            var evs = await _eventRepository.GetAllEventsAsync();
            return evs;
        }
        [HttpPost]
        [Route("GetEventListByIds")]
        public async Task<IEnumerable<EventModel>> GetEventListByIds(List<Guid> ids)
        {
            var evs = await _eventRepository.GetEventsByIdsAsync(ids);
            return evs;
        }

        [HttpPost]
        [Route("GetEventById")]
        public async Task<EventModel> GetEventById(Guid id)
        {
            var ev = await _eventRepository.GetEventByIdAsync(id);
            return ev;
        }

        [HttpPost]
        [Route("GetEventsByUser")]
        public async Task<List<EventModel>> GetEventsByUser(string login)
        {
            var evs = await _eventRepository.GetEventsByUserAsync(login);
            return evs;
        }

        [HttpPost]
        [Route("AddEvent")]
        public async Task<IActionResult> AddEvent(EventModel ev)
        {
            var newEv = new EventModel
            {
                EventHeader = ev.EventHeader,
                EventDescription = ev.EventDescription,
                EventUsersLogins = ev.EventUsersLogins,
            };

            await _eventRepository.AddEventAsync(newEv);

            return Ok();
        }

        [HttpPost]
        [Route("EditEvent")]
        public async Task<bool> EditEvent(EventModel ev)
        {
            return await _eventRepository.EditEventAsync(ev);
        }

        [HttpPost]
        [Route("RemoveEvent")]
        public async Task RemoveEvent(Guid id)
        {
            await _eventRepository.RemoveEventAsync(id);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> RegisterUser(RegisterDto user)
        {

            if (user == null)
            {
                return BadRequest("User or Account Data is Null!!");
            }

            await _userService.RegisterUser(user);
            return Ok();
        }
        [HttpPost]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser(UpdateUserDto user)
        {
            if (user == null)
            {
                return BadRequest("User or Account Data is Null!!");
            }

            if (await _userService.EditUser(user))
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [Route("LoginCheck")]
        public async Task<bool> LogInCheck(LogInDto loginData)
        {

            if (loginData == null)
            {
                return false;
            }

            bool result = await _userService.LogInCheck(loginData.Login, loginData.Password);
            return result;

        }


        [HttpGet]
        [Route("GetAllGroups")]
        public async Task<List<GroupModel>> GetAllGroupsAsync()
        {
            return await _groupService.GetAllGroups();
        }

        [HttpPost]
        [Route("GetUsersFromGroup")]
        public async Task<List<UserModel>> GetUsersFromGroupAsync(Guid grId)
        {
            return await _groupService.GetUsersFromGroup(grId);
        }

        [HttpPost]
        [Route("GetUserGroupsByLogin")]
        public async Task<List<GroupModel>> GetUserGroupsByLoginAsync(string login)
        {
            return await _groupService.GetUserGroupsByLogin(login);
        }

        [HttpPost]
        [Route("AddGroup")]
        public async Task AddGroup(GroupDto gr)
        {
            await _groupService.AddGroud(gr);
        }

        [HttpPost]
        [Route("EditGroup")]
        public async Task<bool> EditGroup(GroupDto gr)
        {
            return await _groupService.EditGroud(gr);
        }

        [HttpPost]
        [Route("RemoveGroup")]
        public async Task RemoveGroup(GroupDto gr)
        {
            await _groupService.RemoveGroud(gr);
        }
    }
}
