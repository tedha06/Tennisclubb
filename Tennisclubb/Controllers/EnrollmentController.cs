using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic; // Ensure you have this for List<T>
using Tennisclubb.Data;
using Tennisclubb.Models;
using Microsoft.EntityFrameworkCore;

public class EnrollmentController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public EnrollmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // ...  

    [HttpPost]
    public IActionResult AddToHistory(string eventName)
    {
        if (string.IsNullOrEmpty(eventName))
        {
            return Json(new { success = false, message = "Event name is required." });
        }

        var userId = _userManager.GetUserId(User);
        if (userId == null)
        {
            return Json(new { success = false, message = "You must be logged in to enroll in events." });
        }

        var existingEnrollment = _context.EnrollmentHistories
          .Where(e => e.UserId == userId && e.EventName == eventName)
          .FirstOrDefault();

        if (existingEnrollment != null)
        {
            return Json(new { success = false, message = "You are already enrolled in this event." });
        }

        var enrollmentHistory = new EnrollmentHistory
        {
            EventName = eventName,
            UserId = userId
        };

        try
        {
            _context.EnrollmentHistories.Add(enrollmentHistory);
            _context.SaveChanges();
            return Json(new { success = true });
        }
        catch (DbUpdateException ex)
        {
            // Log the inner exception for more details  
            Console.WriteLine(ex.InnerException?.Message);
            return Json(new { success = false, message = "An error occurred while saving enrollment." });
        }
    }

    public IActionResult History()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = _userManager.GetUserId(User);
        var history = _context.EnrollmentHistories
          .Where(e => e.UserId == userId)
          .Select(e => new EnrollmentHistory
          {
              EventName = e.EventName,
              EventDate = e.EventDate,
              CoachName = e.CoachName
          })
          .ToList();

        return View(history);
    }

    [HttpPost]
    public IActionResult SaveChanges()
    {
        return Json(new { success = true });
    }

    public IActionResult EnrollmentHistory()
    {
        // Get logged-in user ID
        var userId = _userManager.GetUserId(User);

        // Fetch the user's enrollments from the database
        var enrollments = _context.EnrollmentHistories
                                  .Where(e => e.UserId == userId)
                                  .ToList();

        return View(enrollments);  // Ensure the view is 'EnrollmentHistory.cshtml' in 'Views/Schedule/'
    }
}
