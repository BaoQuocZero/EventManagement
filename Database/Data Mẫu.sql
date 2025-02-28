INSERT INTO ROLES (NAME, DESCRIPTION, CREATE_AT, UPDATE_AT, IS_DELETE)
VALUES 
(N'Admin', N'Quản trị viên hệ thống', GETDATE(), GETDATE(), 0),
(N'User', N'Người dùng thông thường', GETDATE(), GETDATE(), 0),
(N'Editor', N'Biên tập viên, có quyền chỉnh sửa nội dung', GETDATE(), GETDATE(), 0),
(N'Manager', N'Quản lý, có quyền quản lý người dùng và nội dung', GETDATE(), GETDATE(), 0),
(N'Guest', N'Khách, chỉ có quyền xem nội dung', GETDATE(), GETDATE(), 0);