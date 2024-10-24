using Tennisclubb.Models;

namespace Tennisclubb.Models


{
    public class Enrollment
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } // Link to schedule

        public int MemberId { get; set; }
        public Member Member { get; set; } // Link to member
    }
}
