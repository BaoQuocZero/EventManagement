using demo_02.Models;
using demo_02.Servers;
using demo_02.Services;
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

//builder.Services.AddControllersWithViews()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//        options.JsonSerializerOptions.WriteIndented = true;
//    });


// Add services to the container.
builder.Services
    .AddRazorPages()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Đăng ký EventManagementContext vào DI container
builder.Services.AddDbContext<EventManagementContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThucTap"));
});

// ✅ Đăng ký NotificationService mới
builder.Services.AddScoped<NotificationService>();

// Đăng ký dịch vụ FakeDataService
builder.Services.AddScoped<FakeDataService>();

builder.Services.AddScoped<EventService>();
// Đăng ký AuthService
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ParticipantsService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();


// Tạo scope để chạy Seeder
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var dbContext = services.GetRequiredService<EventManagementContext>();

//    // Áp dụng migration nếu chưa có
//    dbContext.Database.Migrate();

//    // ✅ Gọi Seeder để thêm dữ liệu mới mỗi lần chạy ứng dụng
//    var seeder = new DatabaseSeeder(dbContext);
//    seeder.Seed();
//}

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

