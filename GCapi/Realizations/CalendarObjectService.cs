using gcapi.DataBase;
using gcapi.Interfaces;
using gcapi.Interfaces.Services;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
namespace gcapi.Realizations
{
    public class CalendarObjectService : ICalendarObjectService
    {
        private readonly gContext _context;
        public CalendarObjectService(gContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> AddEventAsync(EventDto obj)
        {
            try
            {
                var theUser = await _context.UserTable.FindAsync(obj.CalendarObject.Owner);
                var theGroup = await _context.GroupTable.FindAsync(obj.GroupId);
                var newEvent = new EventModel
                {
                    Name = obj.CalendarObject.Name,
                    DateTimeFrom = obj.CalendarObject.DateTimeFrom,
                    DateTimeTo = obj.CalendarObject.DateTimeTo,
                    HexColor = obj.CalendarObject.HexColor,
                    Description = obj.CalendarObject.Description,
                    Emoji = obj.CalendarObject.Emoji,
                    Group = theGroup,
                    Owner = theUser,
                    Visible = obj.CalendarObject.Visible //
                };
                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public async Task<IActionResult> AddPlanAsync(PlanDto obj)
        {
            try
            {
                var theUser = await _context.UserTable.FindAsync(obj.CalendarObject.Owner);
                var newEvent = new PlanModel
                {
                    Name = obj.CalendarObject.Name,
                    DateTimeFrom = obj.CalendarObject.DateTimeFrom,
                    DateTimeTo = obj.CalendarObject.DateTimeTo,
                    HexColor = obj.CalendarObject.HexColor,
                    Description = obj.CalendarObject.Description,
                    Emoji = obj.CalendarObject.Emoji,
                    Owner = theUser,
                    Visible = obj.CalendarObject.Visible
                };
                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public async Task<IActionResult> EditEventAsync(EventDto obj)
        {

            var theEvent = await _context.EventTable.FindAsync(obj.Id);

            if (theEvent != null)
            {
                theEvent.Name = obj.CalendarObject.Name;
                theEvent.Description = obj.CalendarObject.Description;
                theEvent.DateTimeFrom = obj.CalendarObject.DateTimeFrom;
                theEvent.DateTimeTo = obj.CalendarObject.DateTimeTo;
                theEvent.Emoji = obj.CalendarObject.Emoji;
                theEvent.Visible = obj.CalendarObject.Visible;
                _context.Update(theEvent);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestObjectResult("Ивента не существует");
        }

        public async Task<IActionResult> EditPlanAsync(PlanDto obj)
        {

            var thePlan = await _context.PlanTable.FindAsync(obj.Id);

            if (thePlan != null)
            {
                thePlan.Name = obj.CalendarObject.Name;
                thePlan.Description = obj.CalendarObject.Description;
                thePlan.DateTimeFrom = obj.CalendarObject.DateTimeFrom;
                thePlan.DateTimeTo = obj.CalendarObject.DateTimeTo;
                thePlan.Emoji = obj.CalendarObject.Emoji;
                thePlan.Visible = obj.CalendarObject.Visible;
                _context.Update(thePlan);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            return new BadRequestObjectResult("Плана не существует");
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var res = new List<EventDto>();
            var models =  await _context.EventTable.ToListAsync();
            foreach (var model in models)
            {
                res.Add(new EventDto(model));
            }
            return res;
        }
        public async Task<IEnumerable<PlanDto>> GetAllPlanAsync()
        {
            var res = new List<PlanDto>();
            var models = await _context.PlanTable.ToListAsync();
            foreach (var model in models)
            {
                res.Add(new PlanDto(model));
            }
            return res;
        }

        public async Task<IEnumerable<PlanDto>> GetAllUserPlansAsync(Guid userId)
        {
            //var theUser = await _context.UserTable.FindAsync(userId);
            List<PlanDto> plans = await _context.PlanTable.Where(p => p.Owner.Id == userId).Select(plan => new PlanDto(plan, userId)).ToListAsync();
            return plans;
        }

        public async Task<EventDto> GetEventByIdAsync(Guid id)
        {
            var model = await _context.EventTable.FindAsync(id);
            return new EventDto(model);
        }
        public async Task<List<EventDto>> GetEventsByGroupAsync(Guid id)
        {
            var theGroup = await _context.GroupTable.FindAsync(id);
            var events = await _context.EventTable.Where(e => e.Group == theGroup).Select(e => new EventDto(e)).ToListAsync();
            return events;
        }
        public async Task<List<EventModel>> GetUserEventsAsync(Guid userId)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            return theUser.Events;
        }

        public async Task<IActionResult> RemoveEventAsync(Guid id)
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
                return new OkResult();
            }
            else return new BadRequestObjectResult("Ивента не существует");
        }
    }
}
