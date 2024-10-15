using gcapi.DataBase;
using gcapi.DataBaseModels;
using gcapi.Interfaces;
using gcapi.Interfaces.Services;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Enums;
using gcapi.Dto;
namespace gcapi.Realizations
{
    public class CalendarObjectService : ICalendarObjectService
    {
        private readonly gContext _context;
        public CalendarObjectService(gContext context)
        {
            _context = context;
        }


        public async Task AddEventAsync(CalendarObjectDto obj)
        {
            var newEvent = new EventModel
            {
                Name = obj.Name,
                DateTimeFrom = obj.DateTimeFrom,
                DateTimeTo = obj.DateTimeTo,
                Color = obj.Color,
                Description = obj.Description,
                Emoji = obj.Emoji,
                Group = obj.Group,
                Owner = obj.Owner,
                Visible = obj.Visible
            };
            _context.Add(newEvent);
            await _context.SaveChangesAsync();
        }

        public async Task AddPlanAsync(CalendarObjectDto obj) //на фронте разберемся
        {
            var newEvent = new PlanModel
            {
                Name = obj.Name,
                DateTimeFrom = obj.DateTimeFrom,
                DateTimeTo = obj.DateTimeTo,
                Color = obj.Color,
                Description = obj.Description,
                Emoji = obj.Emoji,
                Owner = obj.Owner,
                Visible = obj.Visible
            };
            _context.Add(newEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EditEventAsync(CalendarObjectDto obj)
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

        public async Task<bool> EditPlanAsync(CalendarObjectDto obj)
        {

            var thePlan = await _context.PlanTable.FindAsync(obj.Id);

            if (thePlan != null)
            {   
                thePlan.Name = obj.Name;
                thePlan.Description = obj.Description;
                thePlan.DateTimeFrom = obj.DateTimeFrom;
                thePlan.DateTimeTo = obj.DateTimeTo;
                thePlan.Emoji = obj.Emoji;
                thePlan.Visible = obj.Visible;
                _context.Update(thePlan);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ICalendarObject>> GetAllEventsAsync()
        {
            return await _context.EventTable.ToListAsync();
        }
        public async Task<IEnumerable<ICalendarObject>> GetAllPlanAsync()
        {
            return await _context.PlanTable.ToListAsync();
        }

        public async Task<IEnumerable<ICalendarObject>> GetAllUserPlansAsync(Guid userId)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            return await _context.PlanTable.Where(p => p.Owner == theUser).ToListAsync();
        }

        public async Task<ICalendarObject> GetEventByIdAsync(Guid id)
        {
            var ev = await _context.EventTable.FindAsync(id);
            if (ev != null)
                return ev;
            else throw new NullReferenceException(); // ох уж эти зеленые подчеркивания
        }

        public async Task<List<EventModel>> GetUserEventsAsync(Guid userId)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            if (theUser != null)
                return theUser.Events;
            else throw new NullReferenceException();
        }

        public async Task RemoveEventAsync(Guid id)
        {
            var theEvent = await _context.EventTable.FindAsync(id);
            if (theEvent != null)
            {
                var theGroups = await _context.GroupTable.Where(g => theEvent.Group == g).ToListAsync();
                foreach (var r in theEvent.Reactions)
                {
                    var u = await _context.UserTable.FindAsync(r.OwnerId);
                    if (u != null)
                    {
                        u.Events.Remove(theEvent);
                        _context.Update(u);
                    }
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
