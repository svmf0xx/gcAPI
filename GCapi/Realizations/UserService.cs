using gcapi.db;
using gcapi.Interfaces;
using gcapi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace gcapi.Realizations
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        public UserService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _context.UserTable.ToListAsync();
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

        public async Task RegisterUser(UserModel user)
        {
            (var passwdHash, var salt) = HashPassword(user.PasswordHash);
            var newUser = new UserModel
            {
                Login = user.Login,
                PasswordHash = passwdHash,
                Salt = salt,
                Role = user.Role,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email,
                Phone = user.Phone,
                TelegramId = user.TelegramId
            };

            _context.UserTable.Add(newUser);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserData(UserModel user)
        {
            //todo
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
