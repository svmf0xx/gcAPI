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
        public string? ownerName { get; set; }
        public string? ownerUsername { get; set; }
        public CalObjectDto CalendarObject { get; set; }

        public PlanDto(PlanModel plan, Guid id)
        {
            Id = plan.Id;
            ownerName = $"{plan.Owner.FirstName} {plan.Owner.SecondName}";
            ownerUsername = plan.Owner.Username;
            CalendarObject = new CalObjectDto()
            {
                Owner = id,
                DateTimeFrom = plan.DateTimeFrom,
                DateTimeTo = plan.DateTimeTo,
                Name = plan.Name,
                Visible = plan.Visible,
                Emoji = plan.Emoji,
                HexColor = plan.HexColor,
                Description = plan.Description
            };
        }

        public PlanDto(PlanModel plan)
        {
            Id = plan.Id;
            ownerName = $"{plan.Owner.FirstName} {plan.Owner.SecondName}";
            ownerUsername = plan.Owner.Username;
            CalendarObject = new CalObjectDto()
            {
                Owner = plan.Owner.Id,
                DateTimeFrom = plan.DateTimeFrom,
                DateTimeTo = plan.DateTimeTo,
                Name = plan.Name,
                Visible = plan.Visible,
                Emoji = plan.Emoji,
                HexColor = plan.HexColor,
                Description = plan.Description
            };
        }

        public PlanDto() //без этого всё сломается
        {                //ну логично, это как сказать что есть влад которому надо дать инсулин, но нет просто влада
        }
    }
}
