using gcapi.DataBase;
using gcapi.DataBaseModels;
using gcapi.Interfaces;
using gcapi.Interfaces.Services;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Enums;
namespace gcapi.Realizations
{
    public class CalendarObjectService : ICalendarObjectService
    {
        private readonly gContext _context;
        public CalendarObjectService(gContext context)
        {
            _context = context;
        }

        public async Task AddCObjAsync(ICalendarObject obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EditCObjAsync(ICalendarObject obj)
        {
            var theEvent = await _context.EventTable.FindAsync(obj.Id);
            if (theEvent != null)
            {
                theEvent.Name = obj.Name;
                theEvent.Description = obj.Description;
                theEvent.DateTimeFrom = obj.DateTimeFrom;
                theEvent.DateTimeTo = obj.DateTimeTo;
                theEvent.Emoji = obj.Emoji;
                theEvent.Visible = obj.Visible;
                _context.Update(theEvent);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ICalendarObject>> GetAllCalObjectsAsync()
        {
            return await _context.EventTable.ToListAsync();
        }
        public async Task<IEnumerable<ICalendarObject>> GetAllUserPlansAsync(Guid userId)
        {
            return await _context.EventTable.Where(o => o.Type == CalendarObjectType.Plan).ToListAsync();
        }

        public async Task<EventModel> GetEventByIdAsync(Guid id)
        {
            var ev = await _context.EventTable.FindAsync(id);
            if (ev != null)
                return ev;
            else throw new NullReferenceException(); // ох уж эти зеленые подчеркивания
        }

        public async Task<List<EventModel>> GetUserEventsAsync(Guid userId)
        {
            UserModel? theUser = await _context.UserTable.FindAsync(userId);
            return theUser.Events;
        }

        public async Task RemoveEventAsync(Guid id)
        {
            EventModel? theEvent = await _context.EventTable.FindAsync(id);
            if (theEvent != null)
            {
                var theGroups = await _context.GroupsTable.Where(g => theEvent.Group == g).ToListAsync();
                foreach (var r in theEvent.Reactions)
                {
                    UserModel u = await _context.UserTable.FindAsync(r.OwnerId);
                    u.Events.Remove(theEvent);
                    _context.Update(u);
                }
                foreach (var gr in theGroups)
                {
                    gr.GroupEvents.Remove(theEvent);
                    _context.Update(gr);

                }

                await _context.SaveChangesAsync();
            }
            else throw new NullReferenceException();
        }
    }
}
