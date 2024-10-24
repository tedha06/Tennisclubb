namespace Tennisclubb.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FullName { get; set; } // Assuming this property holds the member's name
                                             // Other properties like Email, DateOfBirth, etc.
        public string Email { get; set; }
        public bool WantsEmails { get; set; } // To send upcoming specials
        public string Password { get; set; } // You can use Identity for better security
    }
}
