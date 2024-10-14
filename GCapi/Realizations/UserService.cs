using gcapi.DataBase;
using gcapi.DataBaseModels;
using gcapi.Dto;
using gcapi.Interfaces;
using gcapi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace gcapi.Realizations
{
    public class UserService : IUserService
    {
        private readonly gContext _context;
        public UserService(gContext context)
        {
            _context = context;
        }

        public Task<bool> EditUser(UpdateUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogInCheck(string login, string passwd)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(RegisterDto user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUser(UpdateUserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
