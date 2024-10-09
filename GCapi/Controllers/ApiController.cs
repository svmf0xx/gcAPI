using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gcapi.Models;
using gcapi.db;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Specialized;
using Microsoft.AspNetCore.SignalR;
using gcapi.Interfaces.Repos;
using gcapi.Interfaces;
using gcapi.Dto;

namespace gcapi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IEventRepository _eventRepository;
        private readonly IUserService _userService;

        public ApiController(IEventRepository eventRepository, ILogger<ApiController> logger, IUserService userService)
        {
            _eventRepository = eventRepository;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllEvents")]
        public async Task<IEnumerable<EventModel>> GetEvents()
        {
            var evs = await _eventRepository.GetAllEventsAsync();
            return evs;
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users;
        }

        [HttpPost]
        [Route("AddEvent")]
        public async Task<IActionResult> AddEvent(EventModel ev)
        {
            var newEv = new EventModel
            {
                EventHeader = ev.EventHeader,
                EventDescription = ev.EventDescription,
                EventUsersId = ev.EventUsersId,
            };

            await _eventRepository.AddEventAsync(newEv);

            return Ok();
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> RegisterUser(UserModel user)
        {

            if(user == null)
            {
                return BadRequest("User or Account Data is Null!!");
            }

            await _userService.RegisterUser(user);
            return Ok();
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
    }
}
