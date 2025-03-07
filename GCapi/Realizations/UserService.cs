using gcapi.DataBase;
using gcapi.Models;
using gcapi.Dto;
using gcapi.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Timers;
using deniszykov.BaseN;
using OtpNet;

namespace gcapi.Realizations
{
    public class UserService : IUserService
    {
        private readonly gContext _context;
        public UserService(gContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> EditUser(UpdateUserDto user)
        {
            try
            {
                var theUser = await _context.UserTable.FirstOrDefaultAsync(u => u.Username == user.Username);
                if (theUser != null)
                {
                    theUser.FirstName = user.FirstName;
                    theUser.SecondName = user.SecondName;
                    theUser.Email = user.Email;
                    theUser.TgId = user.TgId;
                    theUser.Emoji = user.Emoji;
                    _context.Update(theUser);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                return new BadRequestObjectResult("Пользователя не существует");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        async public Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _context.UserTable
                    //.Include(u => u.Groups)
                    //.Include(u => u.Plans)
                    //.Include(u => u.Events)
                    .ToListAsync(); //с базой в >10 это будет очень глупый метод (миллион данных за один запрос)
        }

        public async Task<UserDto?> GetUserByTgId(long tgid)
        {
            UserModel? user = await _context.UserTable.FirstOrDefaultAsync(u => u.TgId == tgid);
            if (user == null)
                return null;
            return new UserDto(user);
        }
        public async Task<UserDto?> GetUserByUsername(string username)
        {
            UserModel? user = await _context.UserTable.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return null;
            return new UserDto(user);

        }


        public async Task<IActionResult> RemoveUser(Guid userId)
        {
            try
            {
                var theUser = await _context.UserTable.FindAsync(userId);
                if (theUser != null)
                {
                    var theGroups = await _context.GroupTable.Where(g => theUser.Groups.Contains(g)).ToListAsync();
                    var theEvents = await _context.EventTable.Where(ev => theUser.Events.Contains(ev)).ToListAsync();
                    var thePlans = await _context.PlanTable.Where(p => theUser.Plans.Contains(p)).ToListAsync();
                    foreach (var ev in theEvents)
                    {
                        var r = ev.Reactions.Find(r => r.OwnerId == theUser.Id);
                        if (r != null)
                        {
                            ev.Reactions.Remove(r);
                            _context.Update(ev);
                        }

                    }

                    foreach (var p in thePlans)
                    {
                        _context.Remove(p);
                    }

                    foreach (var gr in theGroups)
                    {
                        gr.GroupUsers.Remove(theUser);
                        _context.Update(gr);
                    }

                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else return new BadRequestObjectResult("Юзера не существет");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
