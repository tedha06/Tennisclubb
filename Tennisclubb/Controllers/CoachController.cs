using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tennisclubb.Data;
using Tennisclubb.Models;

public class CoachController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CoachController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: /Coach/Index
    public IActionResult Index()
    {
        // Create a list of coaches with their details hardcoded
        var coachesInSchedule = new List<Coach>
    {
        new Coach { Id = 1, FullName = "Coach Garen", Biography = "Garen is known for his strength-focused tennis training.", PhotoUrl = Url.Content("~/CoachImages/Garen.png") },
        new Coach { Id = 2, FullName = "Coach Ornn", Biography = "Ornn specializes in tennis strength and endurance.", PhotoUrl = Url.Content("~/CoachImages/Ornn.png") },
        new Coach { Id = 3, FullName = "Coach Singed", Biography = "Singed focuses on stamina and advanced tennis techniques.", PhotoUrl = Url.Content("~/CoachImages/Singed.png") },
        new Coach { Id = 4, FullName = "Coach Draven", Biography = "Draven is an expert in ability-focused tennis training.", PhotoUrl = Url.Content("~/CoachImages/Draven.png") },
        new Coach { Id = 5, FullName = "Coach Xayah", Biography = "Xayah works alongside Draven for tennis ability training.", PhotoUrl = Url.Content("~/CoachImages/Xayah.png") },
        new Coach { Id = 6, FullName = "Coach Darius", Biography = "Darius is an expert in advanced tennis techniques.", PhotoUrl = Url.Content("~/CoachImages/Darius.png") }
    };

        // Return the hardcoded list to the view
        return View(coachesInSchedule);
    }

    // API trả về các sự kiện từ Schedules để sử dụng với FullCalendar
    public JsonResult GetEvents()
    {
        var events = _context.Schedules
            .Include(s => s.Coach)
            .Select(s => new
            {
                id = s.Id,
                title = $"{s.EventName} - Coach {s.Coach.FullName}",
                start = s.EventDate.ToString("yyyy-MM-ddTHH:mm:ss"), // Định dạng datetime chuẩn ISO cho FullCalendar
                description = s.EventName
            })
            .ToList();

        return Json(events);
    }

    // GET: /Coach/EditProfile/5
    [HttpGet]
    public IActionResult EditProfile(int id)
    {
        var coach = _context.Coaches.FirstOrDefault(c => c.Id == id);
        if (coach == null)
        {
            return NotFound();
        }
        return View(coach); // Trả về view chỉnh sửa profile thầy cô
    }

    // POST: /Coach/EditProfile
    [HttpPost]
    public IActionResult EditProfile(Coach coach)
    {
        if (ModelState.IsValid)
        {
            _context.Coaches.Update(coach); // Update thông tin thầy cô
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(coach);
    }

    // GET: /Coach/UpcomingSchedules
    public IActionResult UpcomingSchedules()
    {
        var userId = _userManager.GetUserId(User); // Lấy UserId của thầy cô đang đăng nhập
        var coach = _context.Coaches.FirstOrDefault(c => c.UserId == userId);

        if (coach == null)
        {
            return NotFound("Coach not found.");
        }

        // Lấy các lịch dạy sắp tới của thầy cô đăng nhập
        var schedules = _context.Schedules
            .Where(s => s.CoachId == coach.Id && s.EventDate >= DateTime.Now)
            .Include(s => s.Coach)
            .ToList();

        return View(schedules);
    }

    // GET: /Coach/ViewEnrolledMembers/5
    public IActionResult ViewEnrolledMembers(int scheduleId)
    {
        var enrollments = _context.Enrollments
            .Where(e => e.ScheduleId == scheduleId)
            .Include(e => e.Member)
            .ToList();
        return View(enrollments); // Trả về view hiển thị danh sách học viên
    }

    [HttpGet]
    public IActionResult ViewProfile()
    {
        var allCoaches = _context.Coaches.ToList();
        return View(allCoaches); // Pass the list of all coaches to the view
    }

}