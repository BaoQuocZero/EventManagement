INSERT INTO ROLES (NAME, DESCRIPTION, CREATE_AT, UPDATE_AT, IS_DELETE)
VALUES 
(N'Admin', N'Quản trị viên hệ thống', GETDATE(), GETDATE(), 0),
(N'User', N'Người dùng thông thường', GETDATE(), GETDATE(), 0),
(N'Editor', N'Biên tập viên, có quyền chỉnh sửa nội dung', GETDATE(), GETDATE(), 0),
(N'Manager', N'Quản lý, có quyền quản lý người dùng và nội dung', GETDATE(), GETDATE(), 0),
(N'Guest', N'Khách, chỉ có quyền xem nội dung', GETDATE(), GETDATE(), 0);

-- 2. Insert vào bảng USERS
INSERT INTO USERS (ROLES_ID, STUDENT_ID, FULL_NAME, CLASSID, CLASSNAME, EMAIL, PHONE_NUMBER, PASSWORD, CREATE_AT, UPDATE_AT, IS_DELETE) VALUES
(1, 'SV001', N'Nguyễn Văn A', 'CTK42', N'Công nghệ thông tin', 'a@example.com', '0987654321', 'password123', GETDATE(), NULL, 0),
(2, 'SV002', N'Trần Thị B', 'CTK42', N'Công nghệ thông tin', 'b@example.com', '0987654322', 'password123', GETDATE(), NULL, 0),
(3, 'SV003', N'Lê Văn C', 'CTK41', N'Khoa học máy tính', 'c@example.com', '0987654323', 'password123', GETDATE(), NULL, 0),
(4, 'SV004', N'Phạm Thị D', 'CTK40', N'Hệ thống thông tin', 'd@example.com', '0987654324', 'password123', GETDATE(), NULL, 0),
(5, 'SV005', N'Hoàng Văn E', 'CTK39', N'Kỹ thuật phần mềm', 'e@example.com', '0987654325', 'password123', GETDATE(), NULL, 0);
GO

-- 3. Insert vào bảng EVENTTYPES
INSERT INTO EVENTTYPES (EVENTTYPES_NAME, CREATE_AT, UPDATE_AT, IS_DELETE) VALUES
(N'Hội thảo', GETDATE(), NULL, 0),
(N'Workshop', GETDATE(), NULL, 0),
(N'Cuộc thi', GETDATE(), NULL, 0),
(N'Giao lưu', GETDATE(), NULL, 0),
(N'Từ thiện', GETDATE(), NULL, 0);
GO

-- 4. Insert vào bảng EVENTS
INSERT INTO EVENTS (EVENTTYPES_ID, EVENT_NAME, EVENT_TIME, END_TIME, LOCATION, DESCRIPTION, REQUIRED_PARTICIPANTS, MAX_PARTICIPANTS, CREATE_AT, UPDATE_AT, IS_DELETE) VALUES
(1, N'Hội thảo AI', GETDATE(), DATEADD(DAY, 1, GETDATE()), N'Hội trường A', N'Giới thiệu về AI', 50, 200, GETDATE(), NULL, 0),
(2, N'Workshop ASP.NET', GETDATE(), DATEADD(DAY, 1, GETDATE()), N'Phòng Lab B', N'Lập trình với ASP.NET Core', 30, 100, GETDATE(), NULL, 0),
(3, N'Cuộc thi Hackathon', GETDATE(), DATEADD(DAY, 2, GETDATE()), N'Sảnh C', N'Lập trình giải bài toán thực tế', 40, 150, GETDATE(), NULL, 0),
(4, N'Giao lưu IT', GETDATE(), DATEADD(DAY, 1, GETDATE()), N'Nhà Văn Hóa', N'Gặp gỡ các chuyên gia CNTT', 60, 250, GETDATE(), NULL, 0),
(5, N'Chương trình Từ thiện', GETDATE(), DATEADD(DAY, 1, GETDATE()), N'Trường ABC', N'Ủng hộ trẻ em khó khăn', 20, 100, GETDATE(), NULL, 0);
GO

-- 5. Insert vào bảng EVENTPARTICIPATIONS
INSERT INTO EVENTPARTICIPATIONS (EVENTS_ID, USER_ID, PARTICIPATION_STATUS, EARNED_POINTS, PARTICIPATION_TIME, CREATE_AT, UPDATE_AT, IS_DELETE) VALUES
(1, 1, N'Tham gia', 10, GETDATE(), GETDATE(), NULL, 0),
(2, 2, N'Tham gia', 20, GETDATE(), GETDATE(), NULL, 0),
(3, 3, N'Hoàn thành', 30, GETDATE(), GETDATE(), NULL, 0),
(4, 4, N'Tham gia', 15, GETDATE(), GETDATE(), NULL, 0),
(5, 5, N'Tham gia', 25, GETDATE(), GETDATE(), NULL, 0);
GO

-- 6. Insert vào bảng EVENTDONATIONS
INSERT INTO EVENTDONATIONS (PARTICIPATION_ID, AMOUNT, DONATION_DATE, CREATE_AT, UPDATE_AT, IS_DELETE) VALUES
(1, 100000, GETDATE(), GETDATE(), NULL, 0),
(2, 50000, GETDATE(), GETDATE(), NULL, 0),
(3, 200000, GETDATE(), GETDATE(), NULL, 0),
(4, 150000, GETDATE(), GETDATE(), NULL, 0),
(5, 300000, GETDATE(), GETDATE(), NULL, 0);
GO

-- 7. Insert vào bảng NOTIFICATIONTYPES
INSERT INTO NOTIFICATIONTYPES (NAME, DESCRIPTION, CREATE_AT, UPDATE_AT, IS_DELETE) VALUES
(N'Thông báo chung', N'Thông báo chung đến tất cả người dùng', GETDATE(), NULL, 0),
(N'Thông báo sự kiện', N'Thông báo liên quan đến sự kiện', GETDATE(), NULL, 0),
(N'Thông báo cá nhân', N'Thông báo riêng cho từng cá nhân', GETDATE(), NULL, 0),
(N'Thông báo hệ thống', N'Thông báo từ hệ thống', GETDATE(), NULL, 0),
(N'Cảnh báo', N'Cảnh báo về tài khoản hoặc sự kiện', GETDATE(), NULL, 0);
GO

-- 8. Insert vào bảng NOTIFICATIONS
INSERT INTO NOTIFICATIONS (NOTIFICATIONTYPES_ID, TITLE, MESSAGE, STATUS, CREATE_AT, UPDATE_AT, IS_DELETE) VALUES
(1, N'Chào mừng', N'Chào mừng bạn đến với hệ thống!', N'Đã gửi', GETDATE(), NULL, 0),
(2, N'Sự kiện sắp diễn ra', N'Hội thảo AI sắp bắt đầu!', N'Đã gửi', GETDATE(), NULL, 0),
(3, N'Bạn có tin nhắn mới', N'Bạn có một thông báo cá nhân mới.', N'Đã gửi', GETDATE(), NULL, 0),
(4, N'Cập nhật hệ thống', N'Hệ thống sẽ bảo trì vào ngày mai.', N'Đã gửi', GETDATE(), NULL, 0),
(5, N'Cảnh báo bảo mật', N'Có hoạt động đăng nhập bất thường.', N'Chưa đọc', GETDATE(), NULL, 0);
GO

-- 9. Insert vào bảng USER_NOTIFICATIONS
INSERT INTO USER_NOTIFICATIONS (NOTIFICATIONS_ID, USER_ID, STATUS, CREATE_AT, UPDATE_AT, IS_DELETE) VALUES
(1, 1, N'Đã đọc', GETDATE(), NULL, 0),
(2, 2, N'Đã đọc', GETDATE(), NULL, 0),
(3, 3, N'Chưa đọc', GETDATE(), NULL, 0),
(4, 4, N'Chưa đọc', GETDATE(), NULL, 0),
(5, 5, N'Đã đọc', GETDATE(), NULL, 0);
GO