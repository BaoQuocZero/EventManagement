using demo_02.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Thêm Session vào services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Session hết hạn sau 60 phút
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services
    .AddRazorPages()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Đăng ký EventManagementContext vào DI container
builder.Services.AddDbContext<EventManagementContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThucTap"));
});

builder.Services.AddScoped<EventService>();
// Đăng ký AuthService
builder.Services.AddScoped<AuthService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

// ✅ Thêm middleware session vào pipeline xử lý HTTP
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
