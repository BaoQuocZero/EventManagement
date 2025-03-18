using front_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace front_end.Controllers
{
    [Route("Events")]
    public class EventsController : Controller
    {
        private readonly EventManagementContext _context;

        public EventsController(EventManagementContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var eventManagementContext = _context.Events.Include(e => e.Eventtypes);
            return View(await eventManagementContext.ToListAsync());
        }

        // GET: Events/Details/5
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("ID is null");
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Eventtypes)
                .FirstOrDefaultAsync(m => m.EventsId == id);

            if (@event == null)
            {
                Console.WriteLine($"Event with ID {id} not found in the database");
                return NotFound();
            }

            Console.WriteLine($"Event found: {@event.EventName}");
            return View(@event);
        }


        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["EventtypesId"] = new SelectList(_context.Eventtypes, "EventtypesId", "EventtypesId");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventsId,EventtypesId,EventName,EventTime,EndTime,Location,Description,RequiredParticipants,MaxParticipants,CreateAt,UpdateAt,IsDelete")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventtypesId"] = new SelectList(_context.Eventtypes, "EventtypesId", "EventtypesId", @event.EventtypesId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["EventtypesId"] = new SelectList(_context.Eventtypes, "EventtypesId", "EventtypesId", @event.EventtypesId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventsId,EventtypesId,EventName,EventTime,EndTime,Location,Description,RequiredParticipants,MaxParticipants,CreateAt,UpdateAt,IsDelete")] Event @event)
        {
            if (id != @event.EventsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventtypesId"] = new SelectList(_context.Eventtypes, "EventtypesId", "EventtypesId", @event.EventtypesId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Eventtypes)
                .FirstOrDefaultAsync(m => m.EventsId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventsId == id);
        }
    }
}
