using Microsoft.AspNetCore.Mvc;
using gcapi.Models;
using gcapi.Dto;
using gcapi.Interfaces.Services;

namespace gcapi.Controllers
{
    [Route("api/User")]
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

        [HttpGet]
        [Route("GetByTgId")]
        public async Task<UserDto?> GetUserByTgId(long tgid)
        {
            return await _userService.GetUserByTgId(tgid);
        }

        [HttpGet]
        [Route("GetByUsername")]
        public async Task<UserDto?> GetUserByUsername(string username)
        {
            return await _userService.GetUserByUsername(username);
        }

        [HttpPost]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser(UpdateUserDto user)
        {
            return await _userService.EditUser(user);
        }

        [HttpDelete]
        [Route("RemoveUser")]
        public async Task<IActionResult> RemoveUser(Guid userId)
        {
            return await _userService.RemoveUser(userId);
        }

    }
}
