using gcapi.DataBase;
using gcapi.Interfaces;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Dto;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using gcapi.DataBaseModels;

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

        public Task<bool> EditGroud(GroupDto gr)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupModel>> GetAllGroups()
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupModel>> GetUserGroupsByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> GetUsersFromGroup(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveGroud(GroupDto gr)
        {
            throw new NotImplementedException();
        }
    }
}
