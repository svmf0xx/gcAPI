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
        public async Task<ResponseRegisterDto> CreateUserTg(RegisterDto user)
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
            //надеюсь я не поломал авторизацию в тг
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ResponseRegisterDto> CreateUser(RegisterDto user)
        {
            return await _authService.RegisterUser(user);
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<UserDto?> LoginUser(LogInDto user)
        {
            return await _authService.LoginUser(user);
        }

        [HttpGet]
        [Route("CheckLogin")]
        public bool CheckLogin(string login)
        {
            return _authService.CheckLogin(login);
        }

        [HttpGet]
        [Route("RecoverUserTokenSigned")]
        public async Task<RecoverUserTokenDto> RecoverUserToken(Guid userId)
        {
            return await _authService.RecoverUserToken(userId);
        }

        [HttpGet]
        [Route("RecoverUserTokenLogin")]
        public async Task<RecoverUserTokenDto> RecoverUserToken(string name)
        {
            return await _authService.RecoverUserToken(name);
        }
    }
}
