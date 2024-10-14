using gcapi.Models;

namespace gcapi.Interfaces.Services
{
    public interface ICalendarObjectService
    {
        public Task AddEventAsync(EventModel ev);
        public Task<bool> EditEventAsync(EventModel ev);
        public Task<IEnumerable<EventModel>> GetAllEventsAsync();
        public Task<EventModel> GetEventByIdAsync(Guid id);
        public Task<List<EventModel>> GetUserEventsAsync(Guid userId);
        public Task RemoveEventAsync(Guid id);
    }
}
