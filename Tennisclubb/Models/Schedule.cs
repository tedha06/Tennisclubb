namespace Tennisclubb.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int CoachId { get; set; }
        public Coach Coach { get; set; }  // Coach entity navigation

    }
}