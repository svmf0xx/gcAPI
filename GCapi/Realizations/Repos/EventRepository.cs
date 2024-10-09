using gcapi.DataBase;
using gcapi.Dto;
using gcapi.Interfaces.Repos;
using gcapi.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System.Threading.Tasks;
namespace gcapi.Realizations.Repos
{
    public class EventRepository : IEventRepository
    {
        private readonly gContext _context;
        public EventRepository(gContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventModel>> GetAllEventsAsync()
        {
            var evs = await _context.EventTable.ToListAsync();
            return evs;
        }
        public async Task AddEventAsync(EventModel ev)
        {
            var newEv = new EventModel
            {
                EventHeader = ev.EventHeader,
                EventDescription = ev.EventDescription,
                EventUsersLogins = ev.EventUsersLogins
            };
            var eventUsers = await _context.UserTable
                                .Where(u => ev.EventUsersLogins.Contains(u.Login))
                                .ToListAsync();

            _context.EventTable.Add(newEv);
            await _context.SaveChangesAsync();

            foreach (var user in eventUsers)
            {
                if (!user.UserEventsId.Contains(newEv.Id))
                {
                    user.UserEventsId.Add(newEv.Id);
                }
            }
            await _context.SaveChangesAsync();

        }
        public async Task<bool> EditEventAsync(EventModel ev)
        {
            var existingEv = await _context.EventTable.FirstOrDefaultAsync(u => u.Id == ev.Id);

            if (existingEv != null)
            {
                existingEv.EventHeader = ev.EventHeader;
                existingEv.EventDescription = ev.EventDescription;
                existingEv.EventUsersLogins = ev.EventUsersLogins;
                var currentUsers = await _context.UserTable
                                .Where(u => u.UserEventsId.Contains(existingEv.Id))
                                .ToListAsync();

                var newUsers = await _context.UserTable
                                .Where(u => ev.EventUsersLogins.Contains(u.Login))
                                .ToListAsync();

                foreach (var user in currentUsers)
                {
                    if (!newUsers.Any(u => u.Id == user.Id))
                    {
                        user.UserEventsId.Remove(existingEv.Id);
                    }
                }

                foreach (var user in newUsers)
                {
                    if (!user.UserEventsId.Contains(existingEv.Id))
                    {
                        user.UserEventsId.Add(existingEv.Id);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task RemoveEventAsync(Guid id)
        {
            var ev = await _context.EventTable.Where(e => e.Id == id).FirstOrDefaultAsync();

            var eventUsers = await _context.UserTable
                                .Where(u => ev.EventUsersLogins.Contains(u.Login))
                                .ToListAsync();

            foreach (var user in eventUsers)
            {
                if (!eventUsers.Any(u => u.Id == user.Id))
                {
                    user.UserEventsId.Remove(ev.Id);
                }
            }

            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<EventModel>> GetEventsByIdsAsync(List<Guid> ids)
        {
            var evs = await _context.EventTable
                            .Where(e => ids.Contains(e.Id))
                            .ToListAsync();
            return evs;
        }
        public async Task<EventModel> GetEventByIdAsync(Guid id)
        {
            var ev = await _context.EventTable.Where(e => e.Id == id).FirstOrDefaultAsync();
            return ev;
        }

        public async Task<List<EventModel>> GetEventsByUserAsync(string login)
        {
            var ev = await _context.EventTable.Where(e => e.EventUsersLogins.Contains(login)).ToListAsync();
            return ev;
        }
    }
}
