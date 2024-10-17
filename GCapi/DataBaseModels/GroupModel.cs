using gcapi.Models;

namespace gcapi.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public long? TgId { get; set; } //пу-пу-пу... че-то поздно догадался это добавить
        public List<UserModel> GroupUsers { get; set; } = [];
        public List<EventModel> GroupEvents { get; set; } = [];
        //public List<PlanModel> GroupPlans { get; set; } = []; //так, стоп
                                                              //А нахуя тут это?
                                                              //ладно...
    }
}
