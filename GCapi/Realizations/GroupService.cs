using gcapi.DataBase;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Dto;
using gcapi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace gcapi.Realizations
{
    public class GroupService(gContext gContext, IUserService userService) : IGroupService
    {
        private readonly gContext _context = gContext;
        private readonly IUserService _userService = userService;

        public async Task<IActionResult> AddGroup(GroupDto gr)
        {
            try
            {
                var users = await _context.UserTable.Where(u => gr.GroupUsers.Contains(u.Username)).ToListAsync();

                var newGroup = new GroupModel
                {
                    Name = gr.Name,
                    GroupUsers = users
                };
                _context.Add(newGroup);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public async Task<IActionResult> EditGroup(GroupDto gr)
        {
            var users = await _context.UserTable.Where(u => gr.GroupUsers.Contains(u.Username)).ToListAsync();

            var theGroup = await _context.GroupTable.Where(g => g.Id == gr.Id).FirstOrDefaultAsync();
            if (theGroup != null)
            {
                theGroup.Name = gr.Name;
                theGroup.GroupUsers = users;
                _context.Update(theGroup);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestObjectResult("Группы не существует");
        }

        public async Task<List<GroupDto>> GetAllGroups()
        {
            List<GroupDto> result = new List<GroupDto>();
            var groups = await _context.GroupTable.Include(g => g.GroupUsers).ToListAsync();
            foreach (var group in groups)
            {
                result.Add(new GroupDto
                {
                    Id = group.Id,
                    Name = group.Name,
                    GroupUsers = group.GroupUsers.Select(u => u.Username).ToList()
                });
            }
            return result;
        }

        public async Task<List<GroupDto?>> GetUserGroups(Guid userId)
        {
            var theUser = await _context.UserTable.FindAsync(userId);

            if (theUser != null)
            {
                List<GroupDto?> result = new List<GroupDto?>();
                var groups = await _context.GroupTable.Include(g => g.GroupUsers.Contains(theUser)).ToListAsync();
                foreach (var group in groups)
                {
                    result.Add(new GroupDto
                    {
                        Id = group.Id,
                        Name = group.Name,
                        GroupUsers = group.GroupUsers.Select(u => u.Username).ToList()
                    });
                }
                return result;
            }
            else return null;
        }

        public async Task<List<UserModel>> GetUsersFromGroup(Guid id)
        {
            var theGroup = await _context.GroupTable.FindAsync(id);
            if (theGroup != null)
                return theGroup.GroupUsers;

            else throw new NullReferenceException();
        }

        public async Task<IActionResult> RemoveGroup(Guid id)
        {
            var theGroup = await _context.GroupTable.FindAsync(id);
            if (theGroup != null)
            {
                var theEvents = await _context.EventTable.Where(ev => theGroup.GroupEvents.Contains(ev)).ToListAsync();
                var theUsers = await _context.UserTable.Where(u => theGroup.GroupUsers.Contains(u)).ToListAsync();

                foreach (var theUser in theUsers)
                {
                    foreach (var theEvent in theEvents)
                    {
                        theUser.Events.Remove(theEvent);
                        _context.Remove(theEvent);
                        _context.Update(theUser);
                    }
                    theUser.Groups.Remove(theGroup);
                }
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            else return new BadRequestObjectResult("Группы не существует");

        }


        public async Task<GroupDto?> GetGroup(Guid grId)
        {
            var group = await _context.GroupTable.Include(g => g.GroupUsers).Where(g => g.Id == grId).FirstOrDefaultAsync();
            var result = new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                GroupUsers = group.GroupUsers.Select(u => u.Username).ToList()
            };

            return result;

        }


        //invite code
        static string GenerateInvite(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        public async Task<string> GetInvite(Guid grId, Guid userId)
        {
            string rcode;
            var gr = await _context.GroupTable.FindAsync(grId);
            var user = await _context.UserTable.FindAsync(userId);

            var inv = await _context.InviteCodeTable.Where(c => c.Group == gr && c.Owner == user).FirstOrDefaultAsync();

            if (user == null || gr == null)
            {
                return String.Empty;
            }
            if (inv == null)
            {
                var code = GenerateInvite();
                var newInv = new InviteCodeModel
                {
                    Code = code,
                    Owner = user,
                    Group = gr,
                    CreatedAt = DateTime.UtcNow,
                    ExpiredAt = DateTime.UtcNow.AddDays(3)
                };
                _context.InviteCodeTable.Add(newInv);
                rcode = code;
            }
            else
            {
                inv.ExpiredAt = DateTime.UtcNow.AddDays(3);
                _context.InviteCodeTable.Update(inv);
                rcode = inv.Code;
            }
            await _context.SaveChangesAsync();
            return rcode;
        }

        public async Task<Guid?> CheckInvite(string code, Guid userId)
        {
            var user = await _context.UserTable.FindAsync(userId);
            var inv = await _context.InviteCodeTable.Include(c => c.Group).Where(c => c.Code == code).FirstOrDefaultAsync();
            if (inv != null && user != null)
            {
                user.Groups.Add(inv.Group);
                inv.Group.GroupUsers.Add(user);

                _context.UserTable.Update(user);
                _context.GroupTable.Update(inv.Group);

                await _context.SaveChangesAsync();
                return inv.Group.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
