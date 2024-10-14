using gcapi.DataBase;
using gcapi.Dto;
using gcapi.Interfaces;
using gcapi.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System.Threading.Tasks;
namespace gcapi.Realizations
{
    public class EventService : IEventService
    {
        private readonly gContext _context;
        public EventService(gContext context)
        {
            _context = context;
        }

        public Task AddEventAsync(EventModel ev)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditEventAsync(EventModel ev)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventModel>> GetAllEventsAsync()
        {
            var evs = await _context.EventTable.ToListAsync();
            return evs;
        }

        public Task<EventModel> GetEventByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventModel>> GetEventsByIdsAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<EventModel>> GetEventsByUserAsync(Guid user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveEventAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
