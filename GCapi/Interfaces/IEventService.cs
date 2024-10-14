using gcapi.Models;

namespace gcapi.Interfaces
{
    public interface IEventService
    {
        public Task<IEnumerable<EventModel>> GetAllEventsAsync();
        public Task<IEnumerable<EventModel>> GetEventsByIdsAsync(List<Guid> ids);
        public Task<EventModel> GetEventByIdAsync(Guid id);
        public Task AddEventAsync(EventModel ev);
        public Task<List<EventModel>> GetEventsByUserAsync(Guid user);
        public Task<bool> EditEventAsync(EventModel ev);
        public Task RemoveEventAsync(Guid id);
    }
}
