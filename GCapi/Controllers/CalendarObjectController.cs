using gcapi.Dto;
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

        [HttpGet] //зачем???? 
        [Route("GetEventById")]
        public async Task<EventDto> GetEventById(Guid evId)
        {
            return await _objService.GetEventByIdAsync(evId);
        }

        [HttpGet] //зачем???? 
        [Route("GetEventsByGroup")]
        public async Task<List<EventDto>> GetEventsByGroup(Guid gId)
        {
            return await _objService.GetEventsByGroupAsync(gId);
        }

        [HttpGet] //зачем???? 
        [Route("GetUserEventsByDate")]
        public async Task<List<EventDto>> GetUserEventsByDate(Guid uId, DateTime date)
        {
            return await _objService.GetUserEventsByDate(uId, date);
        }

        [HttpGet] //зачем???? 
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

        [HttpPut] //тут???? тут пут
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

        [HttpGet] //пост???? (это же буквально геттеры) ну туда же постится id
        [Route("GetUserPlans")]
        public async Task<IEnumerable<PlanDto>> EditPlan(Guid userId)
        {
            return await _objService.GetAllUserPlansAsync(userId);
        }

        [HttpDelete]
        [Route("RemoveEvent")]
        public async Task RemoveEvent(Guid evId)
        {
            await _objService.RemoveEventAsync(evId);
        }

        [HttpGet]
        [Route("GetAllPlansByMonth")]
        public async Task GetAllPlansByMonth(Guid userId, DateTime date)
        {
            await _objService.GetAllPlansByMonth(userId, date);
        }

        [HttpGet]
        [Route("GetAllPlansByDay")]
        public async Task<List<PlanDto>> GetAllPlansByDay(Guid userId, DateTime date)
        {
           return await _objService.GetAllPlansByDay(userId, date);
        }

        [HttpGet]
        [Route("GetUserPlansByDay")]
        public async Task<List<PlanDto>> GetUserPlansByDay(Guid userId, DateTime date)
        {
            return await _objService.GetUserPlansByDay(userId, date);
        }

        [HttpGet]
        [Route("GetUserPlansByWeek")]
        public async Task<List<PlanDto>> GetUserPlansByWeek(Guid userId, DateTime date)
        {
            return await _objService.GetUserPlansByWeek(userId, date);
        }

        [HttpGet]
        [Route("CheckPlansOverlapEvent")]
        public async Task<List<PlanDto>> CheckPlansOverlapEvent(Guid groupId, DateTime from, DateTime to)
        {
            return await _objService.CheckPlansOverlapEvent(groupId, from, to);
        }
    }
}
