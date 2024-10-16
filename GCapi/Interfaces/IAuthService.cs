using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces
{
    public interface IAuthService
    {
        public Task AuthorizeUser();
        public Task<IActionResult> RegisterUser(UserModel user);
    }
}
