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
using gcapi.Dto;
using gcapi.DataBaseModels;
using gcapi.Realizations;
using gcapi.Interfaces.Services;
using gcapi.Interfaces;

namespace gcapi.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController(ICalendarObjectService eventRepository, ILogger<UsersController> logger, IUserService userService, IGroupService groupService) : ControllerBase
    {
        private readonly ILogger<UsersController> _logger = logger;
        private readonly ICalendarObjectService _eventRepository = eventRepository;
        private readonly IUserService _userService = userService;
        private readonly IGroupService _groupService = groupService;

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _userService.GetAllUsersAsync();
        }


    }


   
}
