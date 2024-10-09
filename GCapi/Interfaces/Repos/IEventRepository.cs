using gcapi.Models;

namespace gcapi.Interfaces.Repos
{
    public interface IEventRepository
    {
        public Task<IEnumerable<EventModel>> GetAllEventsAsync();
        public Task<IEnumerable<EventModel>> GetEventsByIdsAsync(List<Guid> ids);
        public Task<EventModel> GetEventByIdAsync(Guid id);
        public Task AddEventAsync(EventModel ev);
        public Task<List<EventModel>> GetEventsByUserAsync(string login);
        public Task<bool> EditEventAsync(EventModel ev);
        public Task RemoveEventAsync(Guid id);
    }
}
