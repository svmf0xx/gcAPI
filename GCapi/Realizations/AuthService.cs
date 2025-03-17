﻿using gcapi.DataBase;
using gcapi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using gcapi.Models;
using gcapi.Dto;
using deniszykov.BaseN;
using Microsoft.EntityFrameworkCore;
using OtpNet;
namespace gcapi.Realizations
{
    public class AuthService(gContext context) : IAuthService
    {

        private readonly gContext _context = context;


        public async Task<ResponseRegisterDto> RegisterUser(RegisterDto user)
        {
            var usernameExist = await _context.UserTable.Where(u => u.Username == user.Username).AnyAsync();
            if (!usernameExist)
            {
                var userModel = new UserModel
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Email = user.Email,
                    TgId = user.TgId,
                    Username = user.Username,
                    TimeZone = user.TimeZone,
                    Secret = GenerateToken(64)
                };
                try
                {
                    await _context.AddAsync(userModel);
                    await _context.SaveChangesAsync();

                    var urlToken = new OtpUri(OtpType.Totp, Base32Convert.ToBytes(userModel.Secret), userModel.Username).ToString();
                    return new ResponseRegisterDto { Username = user.Username, UrlToken = urlToken };
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return new ResponseRegisterDto { isUsernameExist = true };
            }

        }

        public bool CheckLogin(string login) => _context.UserTable.Where(u => u.Username == login).ToList().Any();

        RecoverUserTokenDto getTokenDto(UserModel theUser)
        {
            var secret = new OtpUri(OtpType.Totp, Base32Convert.ToBytes(theUser.Secret), theUser.Username).ToString();
            return new RecoverUserTokenDto
            {
                Email = theUser.Email,
                Secret = secret,
                FirstName = theUser.FirstName
            };
        }
        public async Task<RecoverUserTokenDto> RecoverUserToken(Guid userId)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            return getTokenDto(theUser);
        }

        public async Task<RecoverUserTokenDto> RecoverUserToken(string name)
        {
            var theUser = await _context.UserTable.FirstOrDefaultAsync(u => u.Username == name);
            return getTokenDto(theUser);
        }

        public static string GenerateToken(int N)
        {

            string AllowChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string token = "";
            Random rnd = new();
            int index;
            for (int i = 0; i < N; i++)
            {
                index = rnd.Next(0, AllowChars.Length);
                token += (AllowChars[index]);
            }

            return token;
        }


        public async Task<UserDto?> LoginUser(LogInDto logindata)
        {
            var user = await _context.UserTable.Where(u => logindata.Username == u.Username).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            var totp = new Totp(Base32Convert.ToBytes(user.Secret));
            var code = totp.ComputeTotp();
            if (code != logindata.Code)
            {
                return null;
            }

            return new UserDto(user);
        }
    }
}
