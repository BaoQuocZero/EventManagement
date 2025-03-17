using System.Diagnostics;
using front_end.Models;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EventManagementContext _context;
        public HomeController(EventManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events);
        }

        public IActionResult Details(int id)
        {
            var eventDetail = _context.Events.FirstOrDefault(e => e.EventsId == id);
            if (eventDetail == null)
            {
                return NotFound();
            }
            return View(eventDetail);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
