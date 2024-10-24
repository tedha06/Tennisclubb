namespace Tennisclubb.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string FullName { get; set; }  // Coach's name
        public string Biography { get; set; }  // Brief description of the coach
        public string PhotoUrl { get; set; }  // URL for coach's photo
        public string UserId { get; set; }  // User ID related to the coach (if applicable)

        // A list of schedules where the coach is teaching
        public ICollection<Schedule> Schedules { get; set; }

        public DateTime DateOfBirth { get; set; }  // Add this property
        public string Nationality { get; set; }    // Add this property

    }
}