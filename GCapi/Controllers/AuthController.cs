using gcapi.DataBaseModels;
using gcapi.Interfaces.Services;
using gcapi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController(ICalendarObjectService eventRepository, ILogger<UsersController> logger, IAuthService authService) : ControllerBase
    {
        private readonly ILogger<UsersController> _logger = logger;
        private readonly ICalendarObjectService _eventRepository = eventRepository;
        private readonly IAuthService _authService = authService;

        [HttpPost]
        [Route("CreateUser")]
        public async Task<bool> CreateUser(UserModel user)
        {
            //типа проверки всякие
            //хз, может перенесёшь их куда-нибудь, не знаю, какой структуре проекта
            //вас учили

            if (user == null || user.Username == null || user.FirstName == null)
            {
                return false;
            }

            return await _authService.RegisterUser(user);

        }
    }
}
