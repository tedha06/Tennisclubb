using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tennisclubb.Data;
using Tennisclubb.Models;
using System.Linq;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Main Admin page
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Admin/CreateSchedule
    [HttpGet]
    public IActionResult CreateSchedule()
    {
        var coaches = _context.Coaches.ToList(); // Fetch all coaches from the database
        ViewBag.Coaches = coaches;
        return View();
    }

    // POST: /Admin/CreateSchedule
    [HttpPost]
    public IActionResult CreateSchedule(Schedule newSchedule)
    {
        if (ModelState.IsValid)
        {
            _context.Schedules.Add(newSchedule);
            _context.SaveChanges();
            return Json(newSchedule); // Return the newly created schedule as JSON
        }

        return BadRequest();
    }

    // GET: /Admin/MatchCoachWithSchedule
    [HttpGet]
    public IActionResult MatchCoachWithSchedule()
    {
        var schedules = _context.Schedules.Include(s => s.Coach).ToList();
        var coaches = _context.Coaches.ToList();
        ViewBag.Coaches = coaches;
        return View("MatchCoachWithSchedule", schedules); // Ensure the correct view name and model.
    }

    // POST: /Admin/MatchCoachWithSchedule
    [HttpPost]
    [HttpPost]
    public IActionResult MatchCoachWithSchedule(int scheduleId, int coachId)
    {
        // Check if the coach exists
        var coach = _context.Coaches.FirstOrDefault(c => c.Id == coachId);
        if (coach == null)
        {
            ModelState.AddModelError("", "Coach not found.");
            var schedulesList = _context.Schedules.Include(s => s.Coach).ToList();
            ViewBag.Coaches = _context.Coaches.ToList();
            return View("MatchCoachWithSchedule", schedulesList);
        }

        // Check if the schedule exists
        var schedule = _context.Schedules.FirstOrDefault(s => s.Id == scheduleId);
        if (schedule == null)
        {
            ModelState.AddModelError("", "Schedule not found.");
            var schedulesList = _context.Schedules.Include(s => s.Coach).ToList();
            ViewBag.Coaches = _context.Coaches.ToList();
            return View("MatchCoachWithSchedule", schedulesList);
        }

        // If the event and coach match, return only the matched event
        if (schedule.CoachId == null || schedule.CoachId == coachId)
        {
            // Assign the coach to the schedule if not already assigned
            schedule.CoachId = coachId;
            _context.SaveChanges();

            // Return only the matched schedule
            var matchedSchedule = _context.Schedules
                .Include(s => s.Coach)
                .Where(s => s.Id == scheduleId)
                .ToList();

            ViewBag.SelectedEvent = $"{schedule.EventName} - {coach.FullName}";
            ViewBag.Coaches = _context.Coaches.ToList();
            return View("MatchCoachWithSchedule", matchedSchedule); // Display only the matched schedule
        }

        ModelState.AddModelError("", "The selected coach does not match this event. Please try again.");
        var schedulesListAgain = _context.Schedules.Include(s => s.Coach).ToList();
        ViewBag.Coaches = _context.Coaches.ToList();
        return View("MatchCoachWithSchedule", schedulesListAgain);
    }


    // GET: /Admin/ViewMembers
    public IActionResult ViewMembers()
    {
        // Get the logged-in user's username
        var userName = User.Identity.Name; // This fetches the logged-in user's username or email

        // Create a list with the current logged-in user's details
        var loggedInUser = new List<MemberViewModel>
    {
        new MemberViewModel { UserName = userName, Email = userName, JoinedDate = DateTime.Now } // Use DateTime instead of string
    };

        // Pass the logged-in user's details to the view
        return View(loggedInUser);
    }

    // Method to display schedules and match coaches
    public IActionResult MatchCoach()
    {
        var schedules = _context.Schedules.Include(s => s.Coach).ToList();
        var coaches = _context.Coaches.ToList();
        ViewBag.Coaches = coaches;
        return View("MatchCoachWithSchedule", schedules); // Ensure the correct view name and model.
    }
}
