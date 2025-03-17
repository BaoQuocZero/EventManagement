using front_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace front_end.Controllers
{
    public class EventtypesController : Controller
    {
        private readonly EventManagementContext _context;

        public EventtypesController(EventManagementContext context)
        {
            _context = context;
        }

        // GET: Eventtypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventtypes.ToListAsync());
        }

        // GET: Eventtypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventtype = await _context.Eventtypes
                .FirstOrDefaultAsync(m => m.EventtypesId == id);
            if (eventtype == null)
            {
                return NotFound();
            }

            return View(eventtype);
        }

        // GET: Eventtypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventtypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventtypesId,EventtypesName,CreateAt,UpdateAt,IsDelete")] Eventtype eventtype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventtype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventtype);
        }

        // GET: Eventtypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventtype = await _context.Eventtypes.FindAsync(id);
            if (eventtype == null)
            {
                return NotFound();
            }
            return View(eventtype);
        }

        // POST: Eventtypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventtypesId,EventtypesName,CreateAt,UpdateAt,IsDelete")] Eventtype eventtype)
        {
            if (id != eventtype.EventtypesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventtype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventtypeExists(eventtype.EventtypesId))
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
            return View(eventtype);
        }

        // GET: Eventtypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventtype = await _context.Eventtypes
                .FirstOrDefaultAsync(m => m.EventtypesId == id);
            if (eventtype == null)
            {
                return NotFound();
            }

            return View(eventtype);
        }

        // POST: Eventtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventtype = await _context.Eventtypes.FindAsync(id);
            if (eventtype != null)
            {
                _context.Eventtypes.Remove(eventtype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventtypeExists(int id)
        {
            return _context.Eventtypes.Any(e => e.EventtypesId == id);
        }
    }
}
