using gcapi.Enums;
using gcapi.Interfaces;
using gcapi.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;

namespace gcapi.Dto
{
    public class PlanDto
    {
        public Guid? Id { get; set; }

        // надеюсь я не нарушаю твой гениальный замысел
        public CalObjectDto CalendarObject { get; set; }

        public PlanDto(PlanModel plan)
        {
            Id = plan.Id;
            CalendarObject = new CalObjectDto()
            {
                Owner = plan.Owner.Id,
                DateTimeFrom = plan.DateTimeFrom,
                DateTimeTo = plan.DateTimeTo,
                Name = plan.Name,
                Visible = plan.Visible,
                Emoji = plan.Emoji,
                Color = plan.Color,
                Description = plan.Description
            };
        }

        public PlanDto() //без этого всё сломается
        {
        }
    }
}
