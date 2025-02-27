using demo_02.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace demo_02.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly EventManagementContext _context;

        public IndexModel(EventManagementContext context)
        {
            _context = context;
        }

        public IList<User> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _context.Users.Include(u => u.Roles).ToListAsync();
        }
    }
}
