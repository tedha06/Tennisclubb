using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tennisclubb.Models;
using Tennisclubb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ScheduleController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ScheduleController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Renders the calendar view
    public IActionResult Index()
    {
        return View();
    }

    // Renders the Enrollment History page
    public IActionResult EnrollmentHistory()
    {
        var userId = _userManager.GetUserId(User);

        // Fetch enrollments for the logged-in user
        var enrollments = _context.EnrollmentHistories
                                  .Where(e => e.UserId == userId)
                                  .ToList();

        // Create the coach list
        var coaches = new List<dynamic>
        {
            new { FullName = "Coach Garen", Biography = "Garen is known for his strength-focused tennis training.", PhotoUrl = Url.Content("~/CoachImages/Garen.png") },
            new { FullName = "Coach Ornn", Biography = "Ornn specializes in tennis strength and endurance.", PhotoUrl = Url.Content("~/CoachImages/Ornn.png") },
            new { FullName = "Coach Singed", Biography = "Singed focuses on stamina and advanced tennis techniques.", PhotoUrl = Url.Content("~/CoachImages/Singed.png") },
            new { FullName = "Coach Draven", Biography = "Draven is an expert in ability-focused tennis training.", PhotoUrl = Url.Content("~/CoachImages/Xayah&Draven.png") },
            new { FullName = "Coach Xayah", Biography = "Xayah works alongside Draven for tennis ability training.", PhotoUrl = Url.Content("~/CoachImages/Xayah&Draven.png") },
            new { FullName = "Coach Darius", Biography = "Darius is an expert in advanced tennis techniques.", PhotoUrl = Url.Content("~/CoachImages/Darius.png") }
        };

        // Create the schedule list
        var schedules = new List<dynamic>
        {
            new { EventName = "Beginner Tennis", EventDate = "Monday", Coach = "Coach Garen" },
            new { EventName = "Strength Training", EventDate = "Tuesday", Coach = "Coach Ornn" },
            new { EventName = "Stamina Training", EventDate = "Wednesday", Coach = "Coach Singed" },
            new { EventName = "Ability Training", EventDate = "Thursday", Coach = "Coach Xayah & Coach Draven" },
            new { EventName = "Advanced Tennis", EventDate = "Friday", Coach = "Coach Darius" },
            new { EventName = "Beginner Competition", EventDate = "Saturday", Coach = "N/A" },
            new { EventName = "Advanced Competition", EventDate = "Sunday", Coach = "N/A" }
        };

        // Create an instance of EnrollmentHistoryModel and populate it
        var model = new Tennisclubb.Models.EnrollmentHistoryModel
        {
            EnrollmentHistories = enrollments, // Assuming enrollments is a list of EnrollmentHistory
            Coaches = coaches,
            Schedules = schedules
        };

        // Pass the model to the view
        return View(model);
    }

    // Returns static events for FullCalendar (recurring weekly schedule)
    public JsonResult GetEvents()
    {
        // Static list of weekly events
        var weeklyEvents = new List<object>
        {
            new { id = 1, title = "Beginner Tennis - Coach Garen", startRecur = "2024-10-07T10:00:00", daysOfWeek = new[] { 1 }, description = "Beginner Tennis Class with Coach Garen" }, // Monday
            new { id = 2, title = "Strength Training - Coach Ornn", startRecur = "2024-10-08T12:00:00", daysOfWeek = new[] { 2 }, description = "Strength Training Class with Coach Ornn" }, // Tuesday
            new { id = 3, title = "Stamina Training - Coach Singh", startRecur = "2024-10-09T12:00:00", daysOfWeek = new[] { 3 }, description = "Stamina Training Class with Coach Singh" }, // Wednesday
            new { id = 4, title = "Ability Training - Coaches Draven & Xayah", startRecur = "2024-10-10T10:00:00", daysOfWeek = new[] { 4 }, description = "Ability Training Class with Coaches Draven & Xayah" }, // Thursday
            new { id = 5, title = "Advanced Tennis - Coach Darius", startRecur = "2024-10-11T10:00:00", daysOfWeek = new[] { 5 }, description = "Advanced Tennis Class with Coach Darius" }, // Friday
            new { id = 6, title = "Beginner Competition", startRecur = "2024-10-12T14:00:00", daysOfWeek = new[] { 6 }, description = "Beginner Competition" }, // Saturday
            new { id = 7, title = "Advanced Competition", startRecur = "2024-10-06T10:00:00", daysOfWeek = new[] { 0 }, description = "Advanced Competition" } // Sunday
        };

        // Fetch dynamic events from the database
        var dbEvents = _context.Schedules.Select(s => new {
            id = s.Id,
            title = s.EventName,
            start = s.EventDate,
        }).ToList();

        // Combine both lists of events
        var allEvents = weeklyEvents.Concat(dbEvents).ToList();

        return Json(allEvents);
    }

    // Handles enrollment in a schedule
    [HttpPost]
    public IActionResult Enroll(int scheduleId)
    {
        var schedule = _context.Schedules.Include(s => s.Coach).FirstOrDefault(s => s.Id == scheduleId);
        if (schedule == null)
        {
            return NotFound(new { success = false, message = "Event not found." });
        }

        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Json(new { success = false, message = "User not logged in." });
        }

        // Check if the user is already enrolled in the event
        var existingEnrollment = _context.EnrollmentHistories
            .FirstOrDefault(e => e.EventId == scheduleId && e.UserId == userId);

        if (existingEnrollment != null)
        {
            return Json(new { success = false, message = "You have already enrolled in this event." });
        }

        // Create a new enrollment record
        var enrollmentHistory = new EnrollmentHistory
        {
            EventId = schedule.Id,
            EventName = schedule.EventName,
            EventDate = schedule.EventDate,
            UserId = userId,
            CoachName = schedule.Coach.FullName // Assuming Coach entity has a FullName property
        };

        _context.EnrollmentHistories.Add(enrollmentHistory);

        try
        {
            _context.SaveChanges();
            return Json(new { success = true, eventName = schedule.EventName, eventDate = schedule.EventDate, coachName = schedule.Coach.FullName });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "An error occurred while saving enrollment: " + ex.Message });
        }
    }

    // Handles form submission when saving selected events
    [HttpPost]
    [HttpPost]
    public IActionResult OnPostSaveChanges(List<string> SelectedEvents)
    {
        var userId = _userManager.GetUserId(User);

        if (SelectedEvents != null && SelectedEvents.Count > 0)
        {
            foreach (var eventName in SelectedEvents)
            {
                // Fetch the event
                var schedule = _context.Schedules.FirstOrDefault(s => s.EventName == eventName);
                if (schedule == null) continue;

                // Check if the user is already enrolled in this event
                var existingEnrollment = _context.EnrollmentHistories
                    .FirstOrDefault(e => e.EventId == schedule.Id && e.UserId == userId);

                if (existingEnrollment == null)
                {
                    // Add new enrollment if it does not exist
                    var newEnrollment = new EnrollmentHistory
                    {
                        EventId = schedule.Id,
                        EventName = schedule.EventName,
                        EventDate = schedule.EventDate,
                        UserId = userId,
                        CoachName = schedule.Coach?.FullName
                    };

                    _context.EnrollmentHistories.Add(newEnrollment);
                }
            }

            // Save all new enrollments to the database
            try
            {
                _context.SaveChanges();
                // Redirect to Enrollment History page after saving
                return RedirectToAction("EnrollmentHistory", "Schedule");
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        return RedirectToAction("EnrollmentHistory", "Schedule"); // Redirect to Enrollment History even if no events were selected
    }
}
