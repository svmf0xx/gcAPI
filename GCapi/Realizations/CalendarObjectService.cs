using gcapi.DataBase;
using gcapi.Interfaces;
using gcapi.Interfaces.Services;
using gcapi.Models;
using Microsoft.EntityFrameworkCore;
using gcapi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
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


        public async Task<IActionResult> AddEventAsync(EventDto obj)
        {
            try
            {
                var theUser = await _context.UserTable.FindAsync(obj.CalendarObject.Owner);
                var theGroup = await _context.GroupTable.Include(g => g.GroupUsers).Where(g => g.Id == obj.GroupId).FirstOrDefaultAsync();
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
                    Visible = obj.CalendarObject.Visible
                };
                foreach (var react in theGroup.GroupUsers)
                {
                    newEvent.Reactions.Add(new ReactionModel { OwnerId = react.Id, Reaction = Enums.Reaction.None });
                }
                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        public async Task<List<PlanDto>> CheckPlansOverlapEvent(Guid groupId, DateTime from, DateTime to)
        {
            var theGroup = await _context.GroupTable.Include(g => g.GroupUsers).FirstOrDefaultAsync(g => g.Id == groupId);
            if (theGroup != null)
            {
                var plans = await _context.PlanTable
                    .Include(p => p.Owner)
                    .Where(p => theGroup.GroupUsers.Contains(p.Owner) && p.DateTimeFrom < to && p.DateTimeTo > from) // && p.Visible != Visible.Private
                    .Select(p => new PlanDto(p))
                    .ToListAsync();

                return plans;
            }
            return null;
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
                theEvent.HexColor = obj.CalendarObject.HexColor;
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
                thePlan.HexColor = obj.CalendarObject.HexColor;
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
            var models = await _context.EventTable.Include(e => e.Owner).Include(e => e.Group).ToListAsync();
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
            List<PlanDto> plans = await _context.PlanTable.Where(p => p.Owner.Id == userId).Select(plan => new PlanDto(plan)).ToListAsync();
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
            var events = await _context.EventTable
                .Include(e => e.Owner)
                .Include(e => e.Reactions)
                .Include(e => e.Group)
                .Where(e => e.Group == theGroup)
                .Select(e => new EventDto(e))
                .ToListAsync();

            return events;
        }
        public async Task<List<EventModel>> GetUserEventsAsync(Guid userId)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            return theUser.Events;
        }
        public async Task<List<EventDto>> GetUserEventsByDate(Guid userId, DateTime date)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            var events = await _context.EventTable.Include(e => e.Owner).Include(e => e.Group)
                .Where(e => e.DateTimeFrom.Date == date.Date && e.Reactions.Any(r => r.OwnerId == userId))
                .Select(e => new EventDto(e))
                .ToListAsync();
            return events;
        }
        public async Task<List<EventDto>> GetUserEventsByMonth(Guid userId, DateTime date)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            var events = await _context.EventTable.Include(e => e.Owner).Include(e => e.Group)
                .Where(e => e.DateTimeFrom.Month == date.Month && e.Reactions.Any(r => r.OwnerId == userId))
                .Select(e => new EventDto(e))
                .ToListAsync();
            return events;
        }

        public async Task<List<PlanDto>> GetUserPlansByWeek(Guid userId, DateTime date)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            var plans = await _context.PlanTable.Include(p => p.Owner)
                .Where(p => p.DateTimeFrom >= date && p.DateTimeFrom <= date.AddDays(8) && p.Owner.Id == userId)
                .Select(p => new PlanDto(p))
                .ToListAsync();
            return plans;
        }

        public async Task<List<PlanDto>> GetUserPlansByDay(Guid userId, DateTime date)
        {
            var theUser = await _context.UserTable.FindAsync(userId);
            var plans = await _context.PlanTable.Include(p => p.Owner)
                .Where(p => p.DateTimeFrom == date && p.Owner.Id == userId)
                .Select(p => new PlanDto(p))
                .ToListAsync();
            return plans;
        }

        public async Task<List<PlanDto>> GetAllPlansByMonth(Guid userId, DateTime date)
        {
            var theUser = await _context.UserTable.Include(u => u.Groups).FirstOrDefaultAsync(u => u.Id == userId);
            var usersFromGroups = await _context.UserTable
                        .Where(u => u.Groups.Any(g => theUser.Groups.Contains(g)))
                        .Distinct()
                        .ToListAsync();

            var plans = await _context.PlanTable
                .Include(p => p.Owner)
                .Where(p => p.DateTimeFrom.Month == date.Month && usersFromGroups.Contains(p.Owner))
                .Select(p => new PlanDto(p))
                .ToListAsync();

            return plans;
        }

        public async Task<List<PlanDto>> GetAllPlansByDay(Guid userId, DateTime date)
        {
            var theUser = await _context.UserTable.Include(u => u.Groups).FirstOrDefaultAsync(u => u.Id == userId);
            var usersFromGroups = await _context.UserTable
                        .Where(u => u.Groups.Any(g => theUser.Groups.Contains(g)))
                        .Distinct()
                        .ToListAsync();

            var plans = await _context.PlanTable
                .Include(p => p.Owner)
                .Where(p => p.DateTimeFrom.Day == date.Day && usersFromGroups.Contains(p.Owner))
                .Select(p => new PlanDto(p))
                .ToListAsync();

            return plans;
        }
        public async Task<IActionResult> RemoveEventAsync(Guid id)
        {
            try
            {
                var theEvent = await _context.EventTable
                    .Include(e => e.Group)
                    .Include(e => e.Owner)
                    .Include(e => e.Reactions)
                    .Where(e => e.Id == id)
                    .FirstOrDefaultAsync();

                if (theEvent != null)
                {
                    theEvent.Owner.Events.Remove(theEvent);

                    foreach (var reaction in theEvent.Reactions.ToList())
                    {
                        _context.Remove(reaction);
                    }

                    var theGroups = await _context.GroupTable
                        .Where(g => theEvent.Group == g)
                        .ToListAsync();

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

                    _context.UserTable.Update(theEvent.Owner);
                    _context.EventTable.Remove(theEvent);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else return new BadRequestObjectResult("Ивента не существует");
            }
            catch
            {
                return new BadRequestObjectResult("Ивента не существует");
            }
        }

        public async Task<IActionResult> RemovePlanAsync(Guid id)
        {
            try
            {
                var thePlan = await _context.PlanTable
                    .Include(e => e.Owner)
                    .Where(e => e.Id == id)
                    .FirstOrDefaultAsync();

                if (thePlan != null)
                {
                    thePlan.Owner.Plans.Remove(thePlan);
                    _context.UserTable.Update(thePlan.Owner);
                    _context.PlanTable.Remove(thePlan);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else return new BadRequestObjectResult("Плана не существует");
            }
            catch
            {
                return new BadRequestObjectResult("Плана не существует");
            }
        }
    }
}
