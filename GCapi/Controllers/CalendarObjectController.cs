using gcapi.Dto;
using gcapi.Enums;
using gcapi.Interfaces;
using gcapi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Controllers
{
    [Route("api/Calendar")]
    [ApiController]
    public class CalendarObjectController(ICalendarObjectService objService)
    {
        private readonly ICalendarObjectService _objService = objService;

        [HttpGet]
        [Route("GetAllEvents")]
        public async Task<IEnumerable<EventDto>> GetAllEvents()
        {
            return await _objService.GetAllEventsAsync();
        }

        [HttpGet]
        [Route("GetEventById")]
        public async Task<EventDto> GetEventById(Guid evId)
        {
            return await _objService.GetEventByIdAsync(evId);
        }

        [HttpGet]
        [Route("GetEventsByGroup")]
        public async Task<List<EventDto>> GetEventsByGroup(Guid gId)
        {
            return await _objService.GetEventsByGroupAsync(gId);
        }

        [HttpGet]
        [Route("GetUserEventsByDate")]
        public async Task<List<EventDto>> GetUserEventsByDate(Guid uId, DateTime date)
        {
            return await _objService.GetUserEventsByDate(uId, date);
        }

        [HttpGet]
        [Route("GetUserEventsByMonth")]
        public async Task<List<EventDto>> GetUserEventsByMonth(Guid uId, DateTime date)
        {
            return await _objService.GetUserEventsByMonth(uId, date);
        }

        [HttpGet]
        [Route("GetAllPlans")]
        public async Task<IEnumerable<PlanDto>> GetAllPlanss()
        {
            return await _objService.GetAllPlanAsync();
        }

        [HttpPut]
        [Route("AddEvent")]
        public async Task<IActionResult> AddEvent(EventDto obj)
        {
            return await _objService.AddEventAsync(obj);
        }

        [HttpPut]
        [Route("AddPlan")]
        public async Task AddPlan(PlanDto obj)
        {
            await _objService.AddPlanAsync(obj);
        }

        [HttpPost]
        [Route("EditEvent")]
        public async Task<IActionResult> EditEvent(EventDto ev)
        {
            return await _objService.EditEventAsync(ev);
        }

        [HttpPost]
        [Route("EditPlan")]
        public async Task<IActionResult> EditPlan(PlanDto ev)
        {
            return await _objService.EditPlanAsync(ev);
        }

        [HttpPost]
        [Route("AddReactionToEvent")]
        public async Task<IActionResult> AddReactionToEvent(AddReactionDto reaction)
        {
            return await _objService.AddReactionAsync(reaction);
        }

        [HttpGet]
        [Route("GetUserPlans")]
        public async Task<IEnumerable<PlanDto>> GetUserPlans(Guid userId)
        {
            return await _objService.GetAllUserPlansAsync(userId);
        }

        [HttpDelete]
        [Route("RemoveEvent")]
        public async Task RemoveEvent(Guid evId)
        {
            await _objService.RemoveEventAsync(evId);
        }

        [HttpDelete]
        [Route("RemovePlan")]
        public async Task RemovePlan(Guid plId)
        {
            await _objService.RemovePlanAsync(plId);
        }

        [HttpGet]
        [Route("GetAllPlansByMonth")]
        public async Task GetAllPlansByMonth(Guid userId, DateTime date)
        {
            await _objService.GetAllPlansByMonth(userId, date);
        }

        [HttpGet]
        [Route("GetUserPlansByRange")]
        public async Task<List<PlanDto>> GetUserPlansByRange(Guid userId, DateTime dateFrom, DateTime dateTo)
        {
            return await _objService.GetUserPlansByTimerange(userId, dateFrom, dateTo);
        }

        [HttpGet]
        [Route("GetGroupPlansByTimerange")]
        public async Task<List<PlanDto>> GetGroupPlansByTimerange(Guid groupId, DateTime from, DateTime to)
        {
            return await _objService.GetGroupPlansByTimerange(groupId, from, to);
        }
    }
}
