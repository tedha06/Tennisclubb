using Tennisclubb.Data;
using Tennisclubb.Models;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        // Add Coaches if not already in the database
        if (!context.Coaches.Any())
        {

            var coachGaren = new Coach
            {
                FullName = "Coach Garen",
                Biography = "Garen is known for his strength-focused tennis training.",
                DateOfBirth = new DateTime(1985, 5, 10),  // Add Date of Birth
                Nationality = "American"  // Add Nationality
            };
            var coachOrnn = new Coach
            {
                FullName = "Coach Ornn",
                Biography = "Ornn specializes in tennis strength and endurance.",
                DateOfBirth = new DateTime(1980, 4, 20),
                Nationality = "Canadian"
            };
            var coachSingh = new Coach
            {
                FullName = "Coach Singed",
                Biography = "Singed focuses on stamina and advanced tennis techniques.",
                DateOfBirth = new DateTime(1975, 9, 15),
                Nationality = "British"
            };
            var coachDraven = new Coach
            {
                FullName = "Coach Draven",
                Biography = "Draven is an expert in ability-focused tennis training.",
                DateOfBirth = new DateTime(1987, 1, 5),
                Nationality = "Australian"
            };
            var coachXayah = new Coach
            {
                FullName = "Coach Xayah",
                Biography = "Xayah works alongside Draven for tennis ability training.",
                DateOfBirth = new DateTime(1989, 11, 2),
                Nationality = "New Zealander"
            };
            var coachDarius = new Coach
            {
                FullName = "Coach Darius",
                Biography = "Darius is an expert in advanced tennis techniques.",
                DateOfBirth = new DateTime(1983, 6, 23),
                Nationality = "French"
            };

            context.Coaches.AddRange(coachGaren, coachOrnn, coachSingh, coachDraven, coachXayah, coachDarius);
            context.SaveChanges();
        }

        // Add Schedules if not already in the database
        if (!context.Schedules.Any())
        {
            var coachGaren = context.Coaches.First(c => c.FullName == "Coach Garen");
            var coachOrnn = context.Coaches.First(c => c.FullName == "Coach Ornn");
            var coachSingh = context.Coaches.First(c => c.FullName == "Coach Singed");
            var coachDraven = context.Coaches.First(c => c.FullName == "Coach Draven");
            var coachXayah = context.Coaches.First(c => c.FullName == "Coach Xayah");
            var coachDarius = context.Coaches.First(c => c.FullName == "Coach Darius");

            context.Schedules.AddRange(
                new Schedule
                {
                    EventName = "Beginner Tennis - Coach Garen",
                    EventDate = new DateTime(2024, 10, 7, 10, 0, 0),
                    Location = "Court 1",
                    Coach = coachGaren
                },
                new Schedule
                {
                    EventName = "Strength Training - Coach Ornn",
                    EventDate = new DateTime(2024, 10, 8, 12, 0, 0),
                    Location = "Court 2",
                    Coach = coachOrnn
                },
                new Schedule
                {
                    EventName = "Stamina Training - Coach Singed",
                    EventDate = new DateTime(2024, 10, 9, 12, 0, 0),
                    Location = "Court 3",
                    Coach = coachSingh
                },
                new Schedule
                {
                    EventName = "Ability Training - Coaches Draven & Xayah",
                    EventDate = new DateTime(2024, 10, 10, 10, 0, 0),
                    Location = "Court 4",
                    Coach = coachDraven // assuming one coach for this example
                },
                new Schedule
                {
                    EventName = "Advanced Tennis - Coach Darius",
                    EventDate = new DateTime(2024, 10, 11, 10, 0, 0),
                    Location = "Court 5",
                    Coach = coachDarius
                },
                new Schedule
                {
                    EventName = "Beginner Competition",
                    EventDate = new DateTime(2024, 10, 12, 14, 0, 0),
                    Location = "Main Court"
                },
                new Schedule
                {
                    EventName = "Advanced Competition",
                    EventDate = new DateTime(2024, 10, 6, 10, 0, 0),
                    Location = "Main Court"
                }
            );

            context.SaveChanges();
        }
    }
}

