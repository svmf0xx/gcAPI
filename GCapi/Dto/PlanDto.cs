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
        public CalObjectDto CalendarObject { get; set; }

        public PlanDto(PlanModel plan)
        {
            Id = plan.Id;
            CalendarObject = new CalObjectDto()
            {
                Owner = plan.Owner.Id,
                OwnerData = new OwnerDto
                {
                    FirstName = plan.Owner.FirstName,
                    SecondName = plan.Owner.SecondName,
                    Username = plan.Owner.Username
                },
                DateTimeFrom = plan.DateTimeFrom,
                DateTimeTo = plan.DateTimeTo,
                Name = plan.Name,
                Visible = plan.Visible,
                Emoji = plan.Emoji,
                HexColor = plan.HexColor,
                Description = plan.Description
            };
        }

        public PlanDto()
        {
        }
    }
}
