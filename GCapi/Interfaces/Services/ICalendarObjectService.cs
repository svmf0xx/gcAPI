using gcapi.Dto;
using gcapi.Models;

namespace gcapi.Interfaces.Services
{
    public interface ICalendarObjectService
    {
        public Task AddEventAsync(CalendarObjectDto obj);
        public Task AddPlanAsync(CalendarObjectDto obj);
        public Task<bool> EditEventAsync(CalendarObjectDto obj);
        public Task<bool> EditPlanAsync(CalendarObjectDto obj);
        public Task<IEnumerable<ICalendarObject>> GetAllEventsAsync();
        public Task<IEnumerable<ICalendarObject>> GetAllPlanAsync();
        public Task<IEnumerable<ICalendarObject>> GetAllUserPlansAsync(Guid userId);
        public Task<ICalendarObject> GetEventByIdAsync(Guid id);
        public Task<List<EventModel>> GetUserEventsAsync(Guid userId);
        public Task RemoveEventAsync(Guid id);
    }
}
