USE [EventManagement]
GO
/****** Object:  UserDefinedDataType [dbo].[CREATED_AT]    Script Date: 2/28/2025 1:45:34 PM ******/
CREATE TYPE [dbo].[CREATED_AT] FROM [datetime] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[IS_DELETED]    Script Date: 2/28/2025 1:45:34 PM ******/
CREATE TYPE [dbo].[IS_DELETED] FROM [bit] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[UPDATED_AT]    Script Date: 2/28/2025 1:45:34 PM ******/
CREATE TYPE [dbo].[UPDATED_AT] FROM [datetime] NULL
GO
/****** Object:  Table [dbo].[EVENTDONATIONS]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EVENTDONATIONS](
	[EVENTDONATIONS_ID] [int] IDENTITY(1,1) NOT NULL,
	[PARTICIPATION_ID] [int] NOT NULL,
	[AMOUNT] [int] NULL,
	[DONATION_DATE] [datetime] NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EVENTPARTICIPATIONS]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EVENTPARTICIPATIONS](
	[PARTICIPATION_ID] [int] IDENTITY(1,1) NOT NULL,
	[EVENTS_ID] [int] NOT NULL,
	[USER_ID] [int] NOT NULL,
	[PARTICIPATION_STATUS] [nvarchar](60) NULL,
	[EARNED_POINTS] [int] NULL,
	[PARTICIPATION_TIME] [datetime] NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EVENTS]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EVENTS](
	[EVENTS_ID] [int] IDENTITY(1,1) NOT NULL,
	[EVENTTYPES_ID] [int] NOT NULL,
	[EVENT_NAME] [nvarchar](255) NULL,
	[EVENT_TIME] [datetime] NULL,
	[END_TIME] [datetime] NULL,
	[LOCATION] [nvarchar](255) NULL,
	[DESCRIPTION] [ntext] NULL,
	[REQUIRED_PARTICIPANTS] [int] NULL,
	[MAX_PARTICIPANTS] [int] NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EVENTTYPES]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EVENTTYPES](
	[EVENTTYPES_ID] [int] IDENTITY(1,1) NOT NULL,
	[EVENTTYPES_NAME] [nvarchar](1000) NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NOTIFICATIONS]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NOTIFICATIONS](
	[NOTIFICATIONS_ID] [int] IDENTITY(1,1) NOT NULL,
	[NOTIFICATIONTYPES_ID] [int] NOT NULL,
	[TITLE] [nvarchar](255) NULL,
	[MESSAGE] [ntext] NULL,
	[STATUS] [nvarchar](60) NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NOTIFICATIONTYPES]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NOTIFICATIONTYPES](
	[NOTIFICATIONTYPES_ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](255) NULL,
	[DESCRIPTION] [ntext] NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ROLES]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLES](
	[ROLES_ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](255) NULL,
	[DESCRIPTION] [ntext] NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER_NOTIFICATIONS]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER_NOTIFICATIONS](
	[NOTIFICATIONS_ID] [int] NOT NULL,
	[USER_ID] [int] NOT NULL,
	[STATUS] [nvarchar](60) NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL,
 CONSTRAINT [PK_USER_NOTIFICATIONS] PRIMARY KEY CLUSTERED 
(
	[NOTIFICATIONS_ID] ASC,
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 2/28/2025 1:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[USER_ID] [int] IDENTITY(1,1) NOT NULL,
	[ROLES_ID] [int] NOT NULL,
	[STUDENT_ID] [nvarchar](20) NULL,
	[FULL_NAME] [nvarchar](255) NULL,
	[CLASSID] [nvarchar](12) NULL,
	[CLASSNAME] [nvarchar](255) NULL,
	[EMAIL] [nvarchar](255) NULL,
	[PHONE_NUMBER] [nvarchar](20) NULL,
	[PASSWORD] [nvarchar](1000) NOT NULL,
	[CREATE_AT] [datetime] NULL,
	[UPDATE_AT] [datetime] NULL,
	[IS_DELETE] [bit] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EVENTDONATIONS] ON 

INSERT [dbo].[EVENTDONATIONS] ([EVENTDONATIONS_ID], [PARTICIPATION_ID], [AMOUNT], [DONATION_DATE], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, 1, 100000, CAST(N'2025-02-28T13:41:13.860' AS DateTime), CAST(N'2025-02-28T13:41:13.860' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTDONATIONS] ([EVENTDONATIONS_ID], [PARTICIPATION_ID], [AMOUNT], [DONATION_DATE], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, 2, 50000, CAST(N'2025-02-28T13:41:13.860' AS DateTime), CAST(N'2025-02-28T13:41:13.860' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTDONATIONS] ([EVENTDONATIONS_ID], [PARTICIPATION_ID], [AMOUNT], [DONATION_DATE], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, 3, 200000, CAST(N'2025-02-28T13:41:13.860' AS DateTime), CAST(N'2025-02-28T13:41:13.860' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTDONATIONS] ([EVENTDONATIONS_ID], [PARTICIPATION_ID], [AMOUNT], [DONATION_DATE], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, 4, 150000, CAST(N'2025-02-28T13:41:13.860' AS DateTime), CAST(N'2025-02-28T13:41:13.860' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTDONATIONS] ([EVENTDONATIONS_ID], [PARTICIPATION_ID], [AMOUNT], [DONATION_DATE], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, 5, 300000, CAST(N'2025-02-28T13:41:13.860' AS DateTime), CAST(N'2025-02-28T13:41:13.860' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[EVENTDONATIONS] OFF
GO
SET IDENTITY_INSERT [dbo].[EVENTPARTICIPATIONS] ON 

INSERT [dbo].[EVENTPARTICIPATIONS] ([PARTICIPATION_ID], [EVENTS_ID], [USER_ID], [PARTICIPATION_STATUS], [EARNED_POINTS], [PARTICIPATION_TIME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, 1, 1, N'Tham gia', 10, CAST(N'2025-02-28T13:41:13.857' AS DateTime), CAST(N'2025-02-28T13:41:13.857' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTPARTICIPATIONS] ([PARTICIPATION_ID], [EVENTS_ID], [USER_ID], [PARTICIPATION_STATUS], [EARNED_POINTS], [PARTICIPATION_TIME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, 2, 2, N'Tham gia', 20, CAST(N'2025-02-28T13:41:13.857' AS DateTime), CAST(N'2025-02-28T13:41:13.857' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTPARTICIPATIONS] ([PARTICIPATION_ID], [EVENTS_ID], [USER_ID], [PARTICIPATION_STATUS], [EARNED_POINTS], [PARTICIPATION_TIME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, 3, 3, N'Hoàn thành', 30, CAST(N'2025-02-28T13:41:13.857' AS DateTime), CAST(N'2025-02-28T13:41:13.857' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTPARTICIPATIONS] ([PARTICIPATION_ID], [EVENTS_ID], [USER_ID], [PARTICIPATION_STATUS], [EARNED_POINTS], [PARTICIPATION_TIME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, 4, 4, N'Tham gia', 15, CAST(N'2025-02-28T13:41:13.857' AS DateTime), CAST(N'2025-02-28T13:41:13.857' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTPARTICIPATIONS] ([PARTICIPATION_ID], [EVENTS_ID], [USER_ID], [PARTICIPATION_STATUS], [EARNED_POINTS], [PARTICIPATION_TIME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, 5, 5, N'Tham gia', 25, CAST(N'2025-02-28T13:41:13.857' AS DateTime), CAST(N'2025-02-28T13:41:13.857' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[EVENTPARTICIPATIONS] OFF
GO
SET IDENTITY_INSERT [dbo].[EVENTS] ON 

INSERT [dbo].[EVENTS] ([EVENTS_ID], [EVENTTYPES_ID], [EVENT_NAME], [EVENT_TIME], [END_TIME], [LOCATION], [DESCRIPTION], [REQUIRED_PARTICIPANTS], [MAX_PARTICIPANTS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, 1, N'Hội thảo AI', CAST(N'2025-02-28T13:41:13.847' AS DateTime), CAST(N'2025-03-01T13:41:13.847' AS DateTime), N'Hội trường A', N'Giới thiệu về AI', 50, 200, CAST(N'2025-02-28T13:41:13.847' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTS] ([EVENTS_ID], [EVENTTYPES_ID], [EVENT_NAME], [EVENT_TIME], [END_TIME], [LOCATION], [DESCRIPTION], [REQUIRED_PARTICIPANTS], [MAX_PARTICIPANTS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, 2, N'Workshop ASP.NET', CAST(N'2025-02-28T13:41:13.847' AS DateTime), CAST(N'2025-03-01T13:41:13.847' AS DateTime), N'Phòng Lab B', N'Lập trình với ASP.NET Core', 30, 100, CAST(N'2025-02-28T13:41:13.847' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTS] ([EVENTS_ID], [EVENTTYPES_ID], [EVENT_NAME], [EVENT_TIME], [END_TIME], [LOCATION], [DESCRIPTION], [REQUIRED_PARTICIPANTS], [MAX_PARTICIPANTS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, 3, N'Cuộc thi Hackathon', CAST(N'2025-02-28T13:41:13.847' AS DateTime), CAST(N'2025-03-02T13:41:13.847' AS DateTime), N'Sảnh C', N'Lập trình giải bài toán thực tế', 40, 150, CAST(N'2025-02-28T13:41:13.847' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTS] ([EVENTS_ID], [EVENTTYPES_ID], [EVENT_NAME], [EVENT_TIME], [END_TIME], [LOCATION], [DESCRIPTION], [REQUIRED_PARTICIPANTS], [MAX_PARTICIPANTS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, 4, N'Giao lưu IT', CAST(N'2025-02-28T13:41:13.847' AS DateTime), CAST(N'2025-03-01T13:41:13.847' AS DateTime), N'Nhà Văn Hóa', N'Gặp gỡ các chuyên gia CNTT', 60, 250, CAST(N'2025-02-28T13:41:13.847' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTS] ([EVENTS_ID], [EVENTTYPES_ID], [EVENT_NAME], [EVENT_TIME], [END_TIME], [LOCATION], [DESCRIPTION], [REQUIRED_PARTICIPANTS], [MAX_PARTICIPANTS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, 5, N'Chương trình Từ thiện', CAST(N'2025-02-28T13:41:13.847' AS DateTime), CAST(N'2025-03-01T13:41:13.847' AS DateTime), N'Trường ABC', N'Ủng hộ trẻ em khó khăn', 20, 100, CAST(N'2025-02-28T13:41:13.847' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[EVENTS] OFF
GO
SET IDENTITY_INSERT [dbo].[EVENTTYPES] ON 

INSERT [dbo].[EVENTTYPES] ([EVENTTYPES_ID], [EVENTTYPES_NAME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, N'Hội thảo', CAST(N'2025-02-28T13:41:13.837' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTTYPES] ([EVENTTYPES_ID], [EVENTTYPES_NAME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, N'Workshop', CAST(N'2025-02-28T13:41:13.837' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTTYPES] ([EVENTTYPES_ID], [EVENTTYPES_NAME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, N'Cuộc thi', CAST(N'2025-02-28T13:41:13.837' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTTYPES] ([EVENTTYPES_ID], [EVENTTYPES_NAME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, N'Giao lưu', CAST(N'2025-02-28T13:41:13.837' AS DateTime), NULL, 0)
INSERT [dbo].[EVENTTYPES] ([EVENTTYPES_ID], [EVENTTYPES_NAME], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, N'Từ thiện', CAST(N'2025-02-28T13:41:13.837' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[EVENTTYPES] OFF
GO
SET IDENTITY_INSERT [dbo].[NOTIFICATIONS] ON 

INSERT [dbo].[NOTIFICATIONS] ([NOTIFICATIONS_ID], [NOTIFICATIONTYPES_ID], [TITLE], [MESSAGE], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, 1, N'Chào mừng', N'Chào mừng bạn đến với hệ thống!', N'Đã gửi', CAST(N'2025-02-28T13:41:13.870' AS DateTime), NULL, 0)
INSERT [dbo].[NOTIFICATIONS] ([NOTIFICATIONS_ID], [NOTIFICATIONTYPES_ID], [TITLE], [MESSAGE], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, 2, N'Sự kiện sắp diễn ra', N'Hội thảo AI sắp bắt đầu!', N'Đã gửi', CAST(N'2025-02-28T13:41:13.870' AS DateTime), NULL, 0)
INSERT [dbo].[NOTIFICATIONS] ([NOTIFICATIONS_ID], [NOTIFICATIONTYPES_ID], [TITLE], [MESSAGE], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, 3, N'Bạn có tin nhắn mới', N'Bạn có một thông báo cá nhân mới.', N'Đã gửi', CAST(N'2025-02-28T13:41:13.870' AS DateTime), NULL, 0)
INSERT [dbo].[NOTIFICATIONS] ([NOTIFICATIONS_ID], [NOTIFICATIONTYPES_ID], [TITLE], [MESSAGE], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, 4, N'Cập nhật hệ thống', N'Hệ thống sẽ bảo trì vào ngày mai.', N'Đã gửi', CAST(N'2025-02-28T13:41:13.870' AS DateTime), NULL, 0)
INSERT [dbo].[NOTIFICATIONS] ([NOTIFICATIONS_ID], [NOTIFICATIONTYPES_ID], [TITLE], [MESSAGE], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, 5, N'Cảnh báo bảo mật', N'Có hoạt động đăng nhập bất thường.', N'Chưa đọc', CAST(N'2025-02-28T13:41:13.870' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[NOTIFICATIONS] OFF
GO
SET IDENTITY_INSERT [dbo].[NOTIFICATIONTYPES] ON 

INSERT [dbo].[NOTIFICATIONTYPES] ([NOTIFICATIONTYPES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, N'Thông báo chung', N'Thông báo chung đến tất cả người dùng', CAST(N'2025-02-28T13:41:13.863' AS DateTime), NULL, 0)
INSERT [dbo].[NOTIFICATIONTYPES] ([NOTIFICATIONTYPES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, N'Thông báo sự kiện', N'Thông báo liên quan đến sự kiện', CAST(N'2025-02-28T13:41:13.863' AS DateTime), NULL, 0)
INSERT [dbo].[NOTIFICATIONTYPES] ([NOTIFICATIONTYPES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, N'Thông báo cá nhân', N'Thông báo riêng cho từng cá nhân', CAST(N'2025-02-28T13:41:13.863' AS DateTime), NULL, 0)
INSERT [dbo].[NOTIFICATIONTYPES] ([NOTIFICATIONTYPES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, N'Thông báo hệ thống', N'Thông báo từ hệ thống', CAST(N'2025-02-28T13:41:13.863' AS DateTime), NULL, 0)
INSERT [dbo].[NOTIFICATIONTYPES] ([NOTIFICATIONTYPES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, N'Cảnh báo', N'Cảnh báo về tài khoản hoặc sự kiện', CAST(N'2025-02-28T13:41:13.863' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[NOTIFICATIONTYPES] OFF
GO
SET IDENTITY_INSERT [dbo].[ROLES] ON 

INSERT [dbo].[ROLES] ([ROLES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, N'Admin', N'Quản trị viên hệ thống', CAST(N'2025-02-28T13:38:46.193' AS DateTime), CAST(N'2025-02-28T13:38:46.193' AS DateTime), 0)
INSERT [dbo].[ROLES] ([ROLES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, N'User', N'Người dùng thông thường', CAST(N'2025-02-28T13:38:46.193' AS DateTime), CAST(N'2025-02-28T13:38:46.193' AS DateTime), 0)
INSERT [dbo].[ROLES] ([ROLES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, N'Editor', N'Biên tập viên, có quyền chỉnh sửa nội dung', CAST(N'2025-02-28T13:38:46.193' AS DateTime), CAST(N'2025-02-28T13:38:46.193' AS DateTime), 0)
INSERT [dbo].[ROLES] ([ROLES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, N'Manager', N'Quản lý, có quyền quản lý người dùng và nội dung', CAST(N'2025-02-28T13:38:46.193' AS DateTime), CAST(N'2025-02-28T13:38:46.193' AS DateTime), 0)
INSERT [dbo].[ROLES] ([ROLES_ID], [NAME], [DESCRIPTION], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, N'Guest', N'Khách, chỉ có quyền xem nội dung', CAST(N'2025-02-28T13:38:46.193' AS DateTime), CAST(N'2025-02-28T13:38:46.193' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[ROLES] OFF
GO
INSERT [dbo].[USER_NOTIFICATIONS] ([NOTIFICATIONS_ID], [USER_ID], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, 1, N'Đã đọc', CAST(N'2025-02-28T13:41:13.877' AS DateTime), NULL, 0)
INSERT [dbo].[USER_NOTIFICATIONS] ([NOTIFICATIONS_ID], [USER_ID], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, 2, N'Đã đọc', CAST(N'2025-02-28T13:41:13.877' AS DateTime), NULL, 0)
INSERT [dbo].[USER_NOTIFICATIONS] ([NOTIFICATIONS_ID], [USER_ID], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, 3, N'Chưa đọc', CAST(N'2025-02-28T13:41:13.877' AS DateTime), NULL, 0)
INSERT [dbo].[USER_NOTIFICATIONS] ([NOTIFICATIONS_ID], [USER_ID], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, 4, N'Chưa đọc', CAST(N'2025-02-28T13:41:13.877' AS DateTime), NULL, 0)
INSERT [dbo].[USER_NOTIFICATIONS] ([NOTIFICATIONS_ID], [USER_ID], [STATUS], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, 5, N'Đã đọc', CAST(N'2025-02-28T13:41:13.877' AS DateTime), NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[USERS] ON 

INSERT [dbo].[USERS] ([USER_ID], [ROLES_ID], [STUDENT_ID], [FULL_NAME], [CLASSID], [CLASSNAME], [EMAIL], [PHONE_NUMBER], [PASSWORD], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (1, 2, N'110121067', N'Triệu Lâm', N'DA21TTA', N'Công nghệ thông tin', N'lntgame123@gmail.com', N'0398450834', N'$2a$11$8iQDFtayuepKQ/lzP.Ydeecs/bb3sUfsc7MF.DtPPNX/cQdqMayc6', CAST(N'2025-02-28T13:40:03.317' AS DateTime), NULL, 0)
INSERT [dbo].[USERS] ([USER_ID], [ROLES_ID], [STUDENT_ID], [FULL_NAME], [CLASSID], [CLASSNAME], [EMAIL], [PHONE_NUMBER], [PASSWORD], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (2, 1, N'SV001', N'Nguyễn Văn A', N'CTK42', N'Công nghệ thông tin', N'a@example.com', N'0987654321', N'password123', CAST(N'2025-02-28T13:41:13.833' AS DateTime), NULL, 0)
INSERT [dbo].[USERS] ([USER_ID], [ROLES_ID], [STUDENT_ID], [FULL_NAME], [CLASSID], [CLASSNAME], [EMAIL], [PHONE_NUMBER], [PASSWORD], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (3, 2, N'SV002', N'Trần Thị B', N'CTK42', N'Công nghệ thông tin', N'b@example.com', N'0987654322', N'password123', CAST(N'2025-02-28T13:41:13.833' AS DateTime), NULL, 0)
INSERT [dbo].[USERS] ([USER_ID], [ROLES_ID], [STUDENT_ID], [FULL_NAME], [CLASSID], [CLASSNAME], [EMAIL], [PHONE_NUMBER], [PASSWORD], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (4, 3, N'SV003', N'Lê Văn C', N'CTK41', N'Khoa học máy tính', N'c@example.com', N'0987654323', N'password123', CAST(N'2025-02-28T13:41:13.833' AS DateTime), NULL, 0)
INSERT [dbo].[USERS] ([USER_ID], [ROLES_ID], [STUDENT_ID], [FULL_NAME], [CLASSID], [CLASSNAME], [EMAIL], [PHONE_NUMBER], [PASSWORD], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (5, 4, N'SV004', N'Phạm Thị D', N'CTK40', N'Hệ thống thông tin', N'd@example.com', N'0987654324', N'password123', CAST(N'2025-02-28T13:41:13.833' AS DateTime), NULL, 0)
INSERT [dbo].[USERS] ([USER_ID], [ROLES_ID], [STUDENT_ID], [FULL_NAME], [CLASSID], [CLASSNAME], [EMAIL], [PHONE_NUMBER], [PASSWORD], [CREATE_AT], [UPDATE_AT], [IS_DELETE]) VALUES (6, 5, N'SV005', N'Hoàng Văn E', N'CTK39', N'Kỹ thuật phần mềm', N'e@example.com', N'0987654325', N'password123', CAST(N'2025-02-28T13:41:13.833' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[USERS] OFF
GO
/****** Object:  Index [PK_EVENTDONATIONS]    Script Date: 2/28/2025 1:45:34 PM ******/
ALTER TABLE [dbo].[EVENTDONATIONS] ADD  CONSTRAINT [PK_EVENTDONATIONS] PRIMARY KEY NONCLUSTERED 
(
	[EVENTDONATIONS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK_EVENTPARTICIPATIONS]    Script Date: 2/28/2025 1:45:34 PM ******/
ALTER TABLE [dbo].[EVENTPARTICIPATIONS] ADD  CONSTRAINT [PK_EVENTPARTICIPATIONS] PRIMARY KEY NONCLUSTERED 
(
	[PARTICIPATION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK_EVENTS]    Script Date: 2/28/2025 1:45:34 PM ******/
ALTER TABLE [dbo].[EVENTS] ADD  CONSTRAINT [PK_EVENTS] PRIMARY KEY NONCLUSTERED 
(
	[EVENTS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK_EVENTTYPES]    Script Date: 2/28/2025 1:45:34 PM ******/
ALTER TABLE [dbo].[EVENTTYPES] ADD  CONSTRAINT [PK_EVENTTYPES] PRIMARY KEY NONCLUSTERED 
(
	[EVENTTYPES_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK_NOTIFICATIONS]    Script Date: 2/28/2025 1:45:34 PM ******/
ALTER TABLE [dbo].[NOTIFICATIONS] ADD  CONSTRAINT [PK_NOTIFICATIONS] PRIMARY KEY NONCLUSTERED 
(
	[NOTIFICATIONS_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK_NOTIFICATIONTYPES]    Script Date: 2/28/2025 1:45:34 PM ******/
ALTER TABLE [dbo].[NOTIFICATIONTYPES] ADD  CONSTRAINT [PK_NOTIFICATIONTYPES] PRIMARY KEY NONCLUSTERED 
(
	[NOTIFICATIONTYPES_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK_ROLES]    Script Date: 2/28/2025 1:45:34 PM ******/
ALTER TABLE [dbo].[ROLES] ADD  CONSTRAINT [PK_ROLES] PRIMARY KEY NONCLUSTERED 
(
	[ROLES_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PK_USERS]    Script Date: 2/28/2025 1:45:34 PM ******/
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [PK_USERS] PRIMARY KEY NONCLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EVENTDONATIONS]  WITH CHECK ADD  CONSTRAINT [FK_EVENTDON_EVENTDONA_EVENTPAR] FOREIGN KEY([PARTICIPATION_ID])
REFERENCES [dbo].[EVENTPARTICIPATIONS] ([PARTICIPATION_ID])
GO
ALTER TABLE [dbo].[EVENTDONATIONS] CHECK CONSTRAINT [FK_EVENTDON_EVENTDONA_EVENTPAR]
GO
ALTER TABLE [dbo].[EVENTPARTICIPATIONS]  WITH CHECK ADD  CONSTRAINT [FK_EVENTPAR_EVENTPART_EVENTS] FOREIGN KEY([EVENTS_ID])
REFERENCES [dbo].[EVENTS] ([EVENTS_ID])
GO
ALTER TABLE [dbo].[EVENTPARTICIPATIONS] CHECK CONSTRAINT [FK_EVENTPAR_EVENTPART_EVENTS]
GO
ALTER TABLE [dbo].[EVENTPARTICIPATIONS]  WITH CHECK ADD  CONSTRAINT [FK_EVENTPAR_EVENTPART_USERS] FOREIGN KEY([USER_ID])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[EVENTPARTICIPATIONS] CHECK CONSTRAINT [FK_EVENTPAR_EVENTPART_USERS]
GO
ALTER TABLE [dbo].[EVENTS]  WITH CHECK ADD  CONSTRAINT [FK_EVENTS_EVENTS_EV_EVENTTYP] FOREIGN KEY([EVENTTYPES_ID])
REFERENCES [dbo].[EVENTTYPES] ([EVENTTYPES_ID])
GO
ALTER TABLE [dbo].[EVENTS] CHECK CONSTRAINT [FK_EVENTS_EVENTS_EV_EVENTTYP]
GO
ALTER TABLE [dbo].[NOTIFICATIONS]  WITH CHECK ADD  CONSTRAINT [FK_NOTIFICA_NOTIFICAT_NOTIFICA] FOREIGN KEY([NOTIFICATIONTYPES_ID])
REFERENCES [dbo].[NOTIFICATIONTYPES] ([NOTIFICATIONTYPES_ID])
GO
ALTER TABLE [dbo].[NOTIFICATIONS] CHECK CONSTRAINT [FK_NOTIFICA_NOTIFICAT_NOTIFICA]
GO
ALTER TABLE [dbo].[USER_NOTIFICATIONS]  WITH CHECK ADD  CONSTRAINT [FK_USER_NOT_USER_NOTI_NOTIFICA] FOREIGN KEY([NOTIFICATIONS_ID])
REFERENCES [dbo].[NOTIFICATIONS] ([NOTIFICATIONS_ID])
GO
ALTER TABLE [dbo].[USER_NOTIFICATIONS] CHECK CONSTRAINT [FK_USER_NOT_USER_NOTI_NOTIFICA]
GO
ALTER TABLE [dbo].[USER_NOTIFICATIONS]  WITH CHECK ADD  CONSTRAINT [FK_USER_NOT_USER_NOTI_USERS] FOREIGN KEY([USER_ID])
REFERENCES [dbo].[USERS] ([USER_ID])
GO
ALTER TABLE [dbo].[USER_NOTIFICATIONS] CHECK CONSTRAINT [FK_USER_NOT_USER_NOTI_USERS]
GO
ALTER TABLE [dbo].[USERS]  WITH CHECK ADD  CONSTRAINT [FK_USERS_USERS_ROL_ROLES] FOREIGN KEY([ROLES_ID])
REFERENCES [dbo].[ROLES] ([ROLES_ID])
GO
ALTER TABLE [dbo].[USERS] CHECK CONSTRAINT [FK_USERS_USERS_ROL_ROLES]
GO
