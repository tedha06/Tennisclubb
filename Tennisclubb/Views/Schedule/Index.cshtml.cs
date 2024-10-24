using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tennisclubb.Data;
using System.Linq;

namespace Tennisclubb.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // This method will fetch events for the FullCalendar
        public IActionResult OnGetEvents()
        {
            var events = _context.Schedules.Select(e => new
            {
                id = e.Id,
                title = e.EventName,
                start = e.EventDate,
            }).ToList();

            return new JsonResult(events);
        }

        public void OnGet()
        {
            // Any other code for the Index page.
        }
    }
}