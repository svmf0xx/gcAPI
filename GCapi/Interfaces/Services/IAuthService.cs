using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces
{
    public interface IAuthService
    {
        bool CheckLogin(string login);
        Task<UserDto?> LoginUser(LogInDto logindata);
        Task<RecoverUserTokenDto> RecoverUserToken(Guid userId);
        Task<RecoverUserTokenDto> RecoverUserToken(string name);
        public Task<ResponseRegisterDto> RegisterUser(RegisterDto user);
    }
}
