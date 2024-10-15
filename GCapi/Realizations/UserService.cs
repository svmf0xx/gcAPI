using gcapi.DataBase;
using gcapi.DataBaseModels;
using gcapi.Dto;
using gcapi.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace gcapi.Realizations
{
    public class UserService : IUserService
    {
        private readonly gContext _context;
        public UserService(gContext context)
        {
            _context = context;
        }

        public async Task<bool> EditUser(UpdateUserDto user)
        {
            var theUser = await _context.UserTable.FindAsync(user.Username);
            if (theUser != null)
            {
                theUser.FirstName = user.FirstName;
                theUser.SecondName = user.SecondName;
                theUser.Email = user.Email;
                theUser.TgId = user.TgId;
                return true;
            }
            return false;
        }

        async public Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _context.UserTable.ToListAsync();
        }

        public async Task RemoveUser(Guid userId)
        {
            UserModel theUser = await _context.UserTable.FindAsync(userId);
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
            }
        }
    }
}
