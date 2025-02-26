# Hệ Thống Quản Lý Sự Kiện

## Tổng Quan
Hệ thống quản lý sự kiện là một ứng dụng web giúp tổ chức, quản lý và tham gia các sự kiện. Hệ thống được xây dựng bằng .NET cho backend và SQL Server Management Studio (SSMS) để quản lý cơ sở dữ liệu.

## Tính Năng
- Xác thực và phân quyền người dùng
- Tạo, chỉnh sửa và xoá sự kiện
- Quản lý vai trò người dùng (Quản trị viên, Người tổ chức, Người tham gia)
- Bảng điều khiển báo cáo và phân tích
- Hệ thống thông báo (email và thông báo trong ứng dụng)

## Công Nghệ Sử Dụng
- **Backend:** .NET (ASP.NET Core, C#)
- **Cơ sở dữ liệu:** Microsoft SQL Server (SSMS)
- **Frontend:** ASP.NET Razor Pages / React.js (tùy chọn)
- **Xác thực:** Identity Framework / JWT
- **Triển khai:** IIS / Azure / AWS

## Cài Đặt & Cấu Hình
### Yêu Cầu
- .NET SDK
- SQL Server và SSMS
- Visual Studio / VS Code

### Các Bước Cài Đặt
1. Clone repository:
   ```sh
   https://github.com/BaoQuocZero/EventManagement.git
   ```
2. Di chuyển vào thư mục dự án:
   ```sh
   cd EventManagement
   ```
3. Khôi phục các gói phụ thuộc:
   ```sh
   dotnet restore
   ```
4. Cấu hình cơ sở dữ liệu:
   - Mở SSMS và tạo một cơ sở dữ liệu mới.
   - Chạy các tập lệnh SQL từ thư mục `Database`.
   - Cập nhật chuỗi kết nối trong `appsettings.json`.
5. Chạy ứng dụng:
   ```sh
   dotnet run
   ```

## Sử Dụng
- Quản trị viên có thể quản lý người dùng, sự kiện và cài đặt hệ thống.
- Người tổ chức có thể tạo và quản lý sự kiện.
- Người dùng có thể duyệt và đăng ký tham gia sự kiện.

## Đóng Góp
Mọi đóng góp đều được hoan nghênh! Vui lòng fork repository và gửi pull request.

## Giấy Phép
Dự án này chưa được cấp phép theo giấy phép MIT.
