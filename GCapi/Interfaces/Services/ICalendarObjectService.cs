using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces.Services
{
    public interface ICalendarObjectService
    {
        Task<IActionResult> AddEventAsync(EventDto obj);
        Task<IActionResult> AddPlanAsync(PlanDto obj);
        Task<IActionResult> EditEventAsync(EventDto obj);
        Task<IActionResult> EditPlanAsync(PlanDto obj);
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<IEnumerable<PlanDto>> GetAllPlanAsync();
        Task<IEnumerable<PlanDto>> GetAllUserPlansAsync(Guid userId);
        Task<EventDto> GetEventByIdAsync(Guid id);
        Task<List<EventModel>> GetUserEventsAsync(Guid userId);
        Task<IActionResult> RemoveEventAsync(Guid id);
    }
}
