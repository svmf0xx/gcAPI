using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces
{
    public interface IAuthService
    {
        bool CheckLogin(string login);
        Task<UserDto?> LoginUser(LogInDto logindata);
        public Task<ResponseRegisterDto> RegisterUser(RegisterDto user);
    }
}
