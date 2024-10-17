using gcapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using gcapi.Models;
using gcapi.Dto;

namespace gcapi.Controllers
{
    [Route("api/Auth")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController(ILogger<UsersController> logger, IAuthService authService) : ControllerBase
    {
        private readonly ILogger<UsersController> _logger = logger;
        private readonly IAuthService _authService = authService;

        [HttpPost]
        [Route("CreateUserTg")]
        public async Task<IActionResult> CreateUserTg(RegisterDto user)
        {
            //типа проверки всякие
            //хз, может перенесёшь их куда-нибудь, не знаю, какой структуре проекта
            //вас учили

            //ну если много разных проверок то в сервис
            //а вообще зачем они тут если можно проверять в _authService и возвращать жопу если все плохо

            //if (user == null || user.Username == null || user.FirstName == null)
            //{
            //    return false;
            //}

            //оставим ради истории

            return await _authService.RegisterUser(user);

        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(RegisterDto user)
        {
            throw new NotImplementedException(); // потом сам короче сделаешь уже полностью через отп
        }

        [HttpGet]
        [Route("CheckAuth")]
        public async Task<IActionResult> CheckAuth(AuthDto user)
        {
            return await _authService.CheckAuth(user);
        }
    }
}
