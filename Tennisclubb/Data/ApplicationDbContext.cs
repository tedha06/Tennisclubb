using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tennisclubb.Models;

namespace Tennisclubb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Member> Members { get; set; }
    public DbSet<EnrollmentHistory> EnrollmentHistories { get; set; }

    }
}