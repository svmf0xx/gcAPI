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
    [Route("api/[controller]")]
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
        [Route("GetEvents")]
        public async Task<IEnumerable<EventModel>> GetEvents()
        {
            var evs = await _eventRepository.GetAllEventsAsync();
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
                EventUsers = ev.EventUsers,
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
        [Route("Login")]
        public async Task<bool> LogIn(LogInDto loginData)
        {

            if (loginData == null)
            {
                return false;
            }

            bool result = await _userService.LogIn(loginData.Login, loginData.Password);

            return result;
            
        }
    }
}
