using gcapi.DataBase;
using gcapi.Interfaces;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Dto;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace gcapi.Realizations
{
    public class GroupService : IGroupService
    {
        private readonly gContext _context;
        private readonly IUserService _userService;
        public GroupService(gContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<List<GroupModel>> GetAllGroups()
        {
            return await _context.GroupsTable.ToListAsync();
        }
        public async Task<List<GroupModel>> GetUserGroupsByLogin(string login)
        {
            return await _context.GroupsTable.Where(g => g.GroupUsersLogins.Contains(login)).ToListAsync();
        }
        public async Task<List<UserModel>> GetUsersFromGroup(Guid id)
        {
            var gr = await _context.GroupsTable.Where(g => g.Id == id).FirstOrDefaultAsync();
            var users = (await _userService.GetAllUsersAsync()).Where(u => gr.GroupUsersLogins.Contains(u.Login)).ToList();
            return users;
        }
        public async Task AddGroud(GroupDto gr)
        {
            var newGr = new GroupModel
            {
                Name = gr.Name,
                GroupUsersLogins = gr.GroupUsersLogins
            };
            _context.GroupsTable.Add(newGr);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> EditGroud(GroupDto gr)
        {
            var oldGr = await _context.GroupsTable.Where(g => g.Id == gr.Id).FirstOrDefaultAsync();
            if (oldGr != null)
            {
                oldGr.Name = gr.Name;
                oldGr.GroupUsersLogins = gr.GroupUsersLogins;
                _context.GroupsTable.Update(oldGr);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveGroud(GroupDto gr)
        {
            var oldGr = _context.GroupsTable.Find(gr.Name);
            if (oldGr != null)
            {
                var users = await _context.UserTable.Where(u => u.UserGroupsIds.Contains(oldGr.Id)).ToListAsync();
                foreach (var user in users)
                {
                    user.UserGroupsIds.Remove(oldGr.Id);
                }
                _context.GroupsTable.Remove(oldGr);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
