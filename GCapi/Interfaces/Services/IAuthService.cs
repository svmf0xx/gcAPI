using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces
{
    public interface IAuthService
    {
        public Task<IActionResult> CheckAuth(AuthDto authData);
        public Task<IActionResult> RegisterUser(RegisterDto user);
    }
}
