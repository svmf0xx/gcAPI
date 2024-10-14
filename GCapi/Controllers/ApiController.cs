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
using gcapi.DataBaseModels;
using gcapi.Realizations;

namespace gcapi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApiController(IEventService eventRepository, ILogger<ApiController> logger, IUserService userService, IGroupService groupService) : ControllerBase
    {
        private readonly ILogger<ApiController> _logger = logger;
        private readonly IEventService _eventRepository = eventRepository;
        private readonly IUserService _userService = userService;
        private readonly IGroupService _groupService = groupService;

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return users;
        }

    }


   
}
