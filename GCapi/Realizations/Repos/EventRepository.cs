using gcapi.db;
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
        private readonly Context _context;
        public EventRepository(Context context)
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
            _context.EventTable.Add(ev);
            await _context.SaveChangesAsync();
        }
    }
}
