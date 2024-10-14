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

    }
}
