using gcapi.Models;

namespace gcapi.Interfaces.Repos
{
    public interface IEventRepository
    {
        public Task<IEnumerable<EventModel>> GetAllEventsAsync();
        public Task AddEventAsync(EventModel ev);

    }
}
