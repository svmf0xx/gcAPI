using gcapi.DataBase;
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

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _context.UserTable.ToListAsync();
        }
        public async Task<IEnumerable<EventModel>> GetAllUsersByEventIdAsync(Guid id)
        {
            return await _context.EventTable.Where(e => e.Id == id).ToListAsync();
        }
        private (string hash, string salt) HashPassword(string passwd)
        {

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string saltStr = Convert.ToBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwd!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return (hashed, saltStr);
        }
        private string HashPassword(string passwd, string salt)
        {
            byte[] _salt = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwd!,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task RegisterUser(RegisterDto user)
        {
            (var passwdHash, var salt) = HashPassword(user.Password);
            var newUser = new UserModel
            {
                Login = user.Login,
                PasswordHash = passwdHash,
                Salt = salt,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email,
                Phone = user.Phone,
                TelegramId = user.TelegramId
            };

            _context.UserTable.Add(newUser);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> EditUser(UpdateUserDto user)
        {
            var existingUser = await _context.UserTable.FirstOrDefaultAsync(u => u.Login == user.Login);

            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.SecondName = user.SecondName;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.TelegramId = user.TelegramId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task RemoveUser(UpdateUserDto user)
        {
            var existingUser = await _context.UserTable.FirstOrDefaultAsync(u => u.Login == user.Login);
            if (existingUser != null)
            {
                _context.UserTable.Remove(existingUser);
            }
            await _context.SaveChangesAsync();
        }
        public async Task<bool> LogInCheck(string login, string passwd)
        {
            var user = await _context.UserTable.FirstOrDefaultAsync(user => user.Login == login);
            if (user == null) return false;

            var passwdHash = HashPassword(passwd, user.Salt);
            if(passwdHash == user.PasswordHash)
            {
                return true;
            }
            return false;
        }

    }
}
