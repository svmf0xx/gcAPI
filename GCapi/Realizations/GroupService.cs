using gcapi.DataBase;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Dto;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using gcapi.DataBaseModels;
using Microsoft.Identity.Client;
using gcapi.Interfaces.Services;
using System.Net;

namespace gcapi.Realizations
{
    public class GroupService : IGroupService
    {
        private readonly gContext _context;
        private readonly IUserService _userService;

        public Task AddGroud(GroupDto gr)
        {
            throw new NotImplementedException();
        }

        public async Task AddGroup(GroupDto gr)
        {
            var users = await _context.UserTable.Where(u => gr.GroupUsers.Contains(u.Username)).ToListAsync();

            var newGroup = new GroupModel
            {
                Name = gr.Name,
                GroupUsers = users
            };
            _context.Add(newGroup);
            await _context.SaveChangesAsync();
        }

        public Task<bool> EditGroud(GroupDto gr)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditGroup(GroupDto gr)
        {
            var users = await _context.UserTable.Where(u => gr.GroupUsers.Contains(u.Username)).ToListAsync();

            var theGroup = await _context.GroupsTable.Where(g => g.Id == gr.Id).FirstOrDefaultAsync();
            if (theGroup != null)
            {
                theGroup.Name = gr.Name;
                theGroup.GroupUsers = users;
                _context.Update(theGroup);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<GroupModel>> GetAllGroups()
        {
            return await _context.GroupsTable.ToListAsync();
        }

        public async Task<List<GroupModel>> GetUserGroups(string username)
        {
            var user = await _context.UserTable.LastAsync(u => u.Username == username);
            return user.Groups ?? [];
            //мне кажется, лучше просто вернуть ничего, чем бить тревогу эксепшном 
            //эксепшены нужно пускать, если что-то идёт ну прям совсем плохо, а тут просто рядовое предсказуемое событие

        }

        public async Task<List<UserModel>> GetUsersFromGroup(Guid id)
        {
            var theGroup = await _context.GroupsTable.FindAsync(id);
            if (theGroup != null)
                return theGroup.GroupUsers;

            else throw new NullReferenceException();
        }

        public async Task<bool> RemoveGroup(Guid id)
        {
            var theGroup = await _context.GroupsTable.FindAsync(id);
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
                return true;
            }
            else throw new NullReferenceException();
            
        }

        public Task<bool> RemoveGroup(GroupDto gr)
        {
            throw new NotImplementedException();
        }
    }
}
