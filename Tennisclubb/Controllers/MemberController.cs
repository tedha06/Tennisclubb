using Microsoft.AspNetCore.Mvc;
using Tennisclubb.Models;

public class MemberController : Controller
{
    // GET: /Member/Index (or simply /Member)
    [HttpGet]
    public IActionResult Index() // Action for the Member homepage
    {
        return View(); // Ensure you have a corresponding view for this action
    }

    // GET: /Member/Create
    [HttpGet]
    public IActionResult Create()
    {
        // Display the create account form
        return View();
    }

    // POST: /Member/Create
    [HttpPost]
    public IActionResult Create(Member member)
    {
        if (ModelState.IsValid)
        {
            // Save the member to the database (you would have database logic here)
            // _context.Members.Add(member);
            // _context.SaveChanges();

            return RedirectToAction("Login");
        }

        return View(member); // Return the same view if the model state is invalid
    }

    // GET: /Member/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Member/Login
    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        // Validate member login (you would have database logic here)
        // var member = _context.Members.SingleOrDefault(m => m.Email == email && m.Password == password);
        // if (member != null)
        if (email == "test@example.com" && password == "password") // Temporary validation logic
        {
            // Redirect to the dashboard if login is successful
            return RedirectToAction("Dashboard");
        }

        // If login fails, return to the login page
        ViewBag.ErrorMessage = "Invalid login attempt";
        return View();
    }

    // GET: /Member/Dashboard
    [HttpGet]
    public IActionResult Dashboard()
    {
        // Display the dashboard after successful login
        return View();
    }

    // POST: /Member/Enroll
    [HttpPost]
    public IActionResult Enroll(int scheduleId)
    {
        // Logic to enroll the member in a schedule (you would add database logic here)
        // Example: Save the member's enrollment into the database

        return RedirectToAction("Index"); // After enrolling, return to the Member Index
    }
}