using demo_02.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_02.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly EventService _eventService;

        public IndexModel(EventService eventService)
        {
            _eventService = eventService;
        }

        public List<User> Users { get; set; }
        public Dictionary<int, string> RoleDictionary { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Lấy danh sách người dùng
            Users = await _eventService.GetAllUsersAsync();

            // Lấy danh sách vai trò và tạo Dictionary để tra cứu nhanh
            var roles = await _eventService.GetRolesAsync();
            RoleDictionary = roles.ToDictionary(r => r.RolesId, r => r.Name);

            // Gán tên vai trò cho từng người dùng
            foreach (var user in Users)
            {
                if (RoleDictionary.TryGetValue(user.RolesId, out var roleName))
                {
                    user.Roles = new Role { Name = roleName }; // Gán thủ công vì chỉ có RolesId trong User
                }
                else
                {
                    user.Roles = new Role { Name = "Không xác định" }; // Tránh lỗi null
                }
            }
        }
    }
}