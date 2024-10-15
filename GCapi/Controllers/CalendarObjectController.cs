using gcapi.Dto;
using gcapi.Interfaces;
using gcapi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace gcapi.Controllers
{
    [Route("api/CalendarObjects")]
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

        [HttpPost]
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

        [HttpPost]
        [Route("AddEvent")]
        public async Task AddEvent(CalendarObjectDto ev)
        {
            await _objService.AddEventAsync(ev);
        }

        [HttpPost]
        [Route("AddPlan")]
        public async Task AddPlan(CalendarObjectDto ev)
        {
            await _objService.AddPlanAsync(ev);
        }

        [HttpPost]
        [Route("EditEvent")]
        public async Task<bool> EditEvent(CalendarObjectDto ev)
        {
            return await _objService.EditEventAsync(ev);
        }

        [HttpPost]
        [Route("EditPlan")]
        public async Task<bool> EditPlan(CalendarObjectDto ev)
        {
            return await _objService.EditPlanAsync(ev);
        }

        [HttpPost]
        [Route("GetUserPlans")]
        public async Task<IEnumerable<ICalendarObject>> EditPlan(Guid userId)
        {
            return await _objService.GetAllUserPlansAsync(userId);
        }

        [HttpPost]
        [Route("RemoveEvent")]
        public async Task RemoveEvent(Guid evId)
        {
            await _objService.RemoveEventAsync(evId);
        }

    }
}
