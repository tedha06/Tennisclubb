namespace Tennisclubb.Models
{
    public class MatchCoachScheduleViewModel
    {
        public int SelectedCoachId { get; set; } // For storing selected coach
        public int SelectedScheduleId { get; set; } // For storing selected schedule
        public IEnumerable<Coach> Coaches { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }

    }
}
