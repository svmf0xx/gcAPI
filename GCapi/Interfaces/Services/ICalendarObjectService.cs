using gcapi.Dto;
using gcapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Interfaces.Services
{
    public interface ICalendarObjectService
    {
        Task<IActionResult> AddEventAsync(EventDto obj);
        Task<IActionResult> AddPlanAsync(PlanDto obj);
        Task<IActionResult> AddReactionAsync(AddReactionDto reaction);
        Task<IActionResult> EditEventAsync(EventDto obj);
        Task<IActionResult> EditPlanAsync(PlanDto obj);
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<IEnumerable<PlanDto>> GetAllPlanAsync();
        Task<List<PlanDto>> GetAllPlansByDay(Guid userId, DateTime date);
        Task<List<PlanDto>> GetAllPlansByMonth(Guid userId, DateTime date);
        Task<IEnumerable<PlanDto>> GetAllUserPlansAsync(Guid userId);
        Task<EventDto> GetEventByIdAsync(Guid id);
        Task<List<EventDto>> GetEventsByGroupAsync(Guid id);
        Task<List<PlanDto>> GetGroupPlansByTimerange(Guid groupId, DateTime dateFrom, DateTime dateTo);
        Task<List<ReactionDto>> GetReactionsForEvent(Guid eventId);
        Task<List<EventModel>> GetUserEventsAsync(Guid userId);
        Task<List<EventDto>> GetUserEventsByDate(Guid userId, DateTime date);
        Task<List<EventDto>> GetUserEventsByRange(Guid userId, DateTime dateFrom, DateTime dateTo);
        Task<List<PlanDto>> GetUserPlansByTimerange(Guid userId, DateTime dateFrom, DateTime dateTo);
        Task<IActionResult> RemoveEventAsync(Guid id);
        Task<IActionResult> RemovePlanAsync(Guid id);
    }
}
