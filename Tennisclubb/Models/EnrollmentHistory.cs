namespace Tennisclubb.Models
{
    public class EnrollmentHistory
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string UserId { get; set; }
        public string CoachName { get; set; }
    }
}
