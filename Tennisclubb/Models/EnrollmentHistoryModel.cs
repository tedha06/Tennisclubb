namespace Tennisclubb.Models
{
    public class EnrollmentHistoryModel
    {
        public List<EnrollmentHistory> EnrollmentHistories { get; set; }
        public List<dynamic> Coaches { get; set; }
        public List<dynamic> Schedules { get; set; }
    }
}