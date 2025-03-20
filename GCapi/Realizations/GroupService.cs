using gcapi.DataBase;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Dto;
using gcapi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.Extensions.Logging;

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
                var userIds = gr.GroupUsers.Select(gu => gu.Id).ToList();
                var users = await _context.UserTable
                          .Where(u => userIds.Contains(u.Id))
                          .ToListAsync();

                var newGroup = new GroupModel
                {
                    Name = gr.Name,
                    GroupUsers = users,
                    Emoji = gr.Emoji,

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
            var userIds = gr.GroupUsers.Select(gu => gu.Id).ToList();
            var users = await _context.UserTable
                      .Where(u => userIds.Contains(u.Id))
                      .ToListAsync();

            var theGroup = await _context.GroupTable.Where(g => g.Id == gr.Id).FirstOrDefaultAsync();
            if (theGroup != null)
            {
                theGroup.Name = gr.Name;
                theGroup.GroupUsers = users;
                theGroup.Emoji = gr.Emoji;

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
                    Emoji = group.Emoji,
                    GroupUsers = group.GroupUsers.Select(u => new UserDto(u)).ToList()
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
                var groups = await _context.GroupTable.Include(g => g.GroupUsers).Include(g => g.GroupEvents).Where(g => g.GroupUsers.Contains(theUser)).ToListAsync();
                foreach (var group in groups)
                {

                    result.Add(new GroupDto
                    {
                        Id = group.Id,
                        Name = group.Name,
                        Emoji = group.Emoji,
                        GroupUsers = group.GroupUsers.Select(u => new UserDto(u)).ToList(),
                        GroupStatistic = GetGroupStatistic(group.GroupEvents),
                    });
                }
                return result;
            }
            else return null;
        }

        public async Task<GroupStatisticDto?> GetGroupStatistic(Guid groupId)
        {
            var group = await _context.GroupTable.Include(g => g.GroupEvents).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null) return null;

            var events = group.GroupEvents;

            var forDay = events.Where(e => e.DateTimeFrom.Date == DateTime.Now.Date).Count();
            var forWeek = events.Where(e => e.DateTimeFrom.Date >= DateTime.Now.Date && e.DateTimeFrom.Date <= DateTime.Now.AddDays(7).Date).Count();
            var forMonth = events.Where(e => e.DateTimeFrom.Date.Month == DateTime.Now.Date.Month && e.DateTimeFrom.Date.Year == DateTime.Now.Date.Year).Count();

            return new GroupStatisticDto()
            {
                EventsForDay = forDay,
                EventsForMonth = forMonth,
                EventsForWeek = forWeek,
            };
        }

        public GroupStatisticDto GetGroupStatistic(List<EventModel> groupEvents)
        {
            var forDay = groupEvents.Where(e => e.DateTimeFrom.Date == DateTime.Now.Date).Count();
            var forWeek = groupEvents.Where(e => e.DateTimeFrom.Date >= DateTime.Now.Date && e.DateTimeFrom.Date <= DateTime.Now.AddDays(7).Date).Count();
            var forMonth = groupEvents.Where(e => e.DateTimeFrom.Date.Month == DateTime.Now.Date.Month && e.DateTimeFrom.Date.Year == DateTime.Now.Date.Year).Count();

            return new GroupStatisticDto()
            {
                EventsForDay = forDay,
                EventsForMonth = forMonth,
                EventsForWeek = forWeek,
            };
        }
        public async Task<List<UserModel>> GetUsersFromGroup(Guid id)
        {
            var theGroup = await _context.GroupTable.FindAsync(id);
            if (theGroup != null)
                return theGroup.GroupUsers;

            else throw new NullReferenceException();
        }


        public async Task<IActionResult> LeaveGroup(Guid userId, Guid groupId)
        {
            var theGroup = await _context.GroupTable.Include(g => g.GroupUsers).FirstOrDefaultAsync(g => g.Id == groupId);
            if (theGroup == null)
            {
                return new BadRequestObjectResult("Группы не существует");
            }

            var theUser = await _context.UserTable.Include(u => u.Events).FirstOrDefaultAsync(u => u.Id == userId);
            if (theUser == null || !theGroup.GroupUsers.Contains(theUser))
            {
                return new BadRequestObjectResult("Пользователь не в группе");
            }

            theGroup.GroupUsers.Remove(theUser);
            theUser.Groups.Remove(theGroup);
            _context.Update(theUser);
            _context.Update(theGroup);

            if (!theGroup.GroupUsers.Any())
            {
                var theEvents = await _context.EventTable.Include(e => e.Reactions).Where(ev => ev.Group.Id == theGroup.Id).ToListAsync();

                foreach (var theEvent in theEvents)
                {
                    _context.RemoveRange(theEvent.Reactions);
                    _context.EventTable.Remove(theEvent);
                }

                _context.GroupTable.Remove(theGroup);
            }

            await _context.SaveChangesAsync();
            return new OkResult();
        }


        public async Task<GroupDto?> GetGroup(Guid grId)
        {
            var group = await _context.GroupTable.Include(g => g.GroupUsers).Where(g => g.Id == grId).FirstOrDefaultAsync();
            var result = new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Emoji = group.Emoji,
                GroupUsers = group.GroupUsers.Select(u => new UserDto(u)).ToList()
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
                var events = await _context.EventTable.Include(e => e.Reactions).Where(e => e.Group == inv.Group).ToListAsync();
                foreach (var ev in events)
                {
                    user.Events.Add(ev);
                    ev.Reactions.Add(new ReactionModel { OwnerId = user.Id, Reaction = Enums.Reaction.None });
                    _context.Update(ev);
                }

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
