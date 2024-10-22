using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces.Services
{
    public interface ICalendarObjectService
    {
        public Task<IActionResult> AddEventAsync(EventDto obj);
        public Task<IActionResult> AddPlanAsync(PlanDto obj);
        public Task<IActionResult> EditEventAsync(EventDto obj);
        public Task<IActionResult> EditPlanAsync(PlanDto obj);
        public Task<IEnumerable<ICalendarObject>> GetAllEventsAsync();
        public Task<IEnumerable<ICalendarObject>> GetAllPlanAsync();
        public Task<IEnumerable<ICalendarObject>> GetAllUserPlansAsync(Guid userId);
        public Task<ICalendarObject> GetEventByIdAsync(Guid id);
        public Task<List<EventModel>> GetUserEventsAsync(Guid userId);
        public Task<IActionResult> RemoveEventAsync(Guid id);
    }
}
