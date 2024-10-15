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
    public class UsersController(ILogger<UsersController> logger, IUserService userService) : ControllerBase
    {
        private readonly ILogger<UsersController> _logger = logger;
        private readonly IUserService _userService = userService;

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpPost]
        [Route("EditUser")]
        public async Task<bool> EditUser(UpdateUserDto user)
        {
            return await _userService.EditUser(user);
        }

        [HttpPost]
        [Route("RemoveUser")]
        public async Task RemoveUser(Guid userId)
        {
            await _userService.RemoveUser(userId);
        }

    }
}
