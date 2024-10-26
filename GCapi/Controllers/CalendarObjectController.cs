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
        public async Task<IEnumerable<ICalendarObject>> GetAllEvents()
        {
            return await _objService.GetAllEventsAsync();
        }

        [HttpGet] //зачем???? 
        [Route("GetEventById")]
        public async Task<ICalendarObject> GetEventById(Guid evId)
        {
            return await _objService.GetEventByIdAsync(evId);
        }

        [HttpGet] 
        [Route("GetAllPlans")]
        public async Task<IEnumerable<ICalendarObject>> GetAllPlanss()
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

    }
}
