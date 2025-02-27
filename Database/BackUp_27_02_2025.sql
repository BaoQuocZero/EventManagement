/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2/27/2025 08:58:00 AM                        */
/*==============================================================*/
use EventManagement

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EVENTDONATIONS') and o.name = 'FK_EVENTDON_EVENTDONA_EVENTPAR')
alter table EVENTDONATIONS
   drop constraint FK_EVENTDON_EVENTDONA_EVENTPAR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EVENTPARTICIPATIONS') and o.name = 'FK_EVENTPAR_EVENTPART_EVENTS')
alter table EVENTPARTICIPATIONS
   drop constraint FK_EVENTPAR_EVENTPART_EVENTS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EVENTPARTICIPATIONS') and o.name = 'FK_EVENTPAR_EVENTPART_USERS')
alter table EVENTPARTICIPATIONS
   drop constraint FK_EVENTPAR_EVENTPART_USERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EVENTS') and o.name = 'FK_EVENTS_EVENTS_EV_EVENTTYP')
alter table EVENTS
   drop constraint FK_EVENTS_EVENTS_EV_EVENTTYP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('NOTIFICATIONS') and o.name = 'FK_NOTIFICA_NOTIFICAT_NOTIFICA')
alter table NOTIFICATIONS
   drop constraint FK_NOTIFICA_NOTIFICAT_NOTIFICA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USERS') and o.name = 'FK_USERS_USERS_ROL_ROLES')
alter table USERS
   drop constraint FK_USERS_USERS_ROL_ROLES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USER_NOTIFICATIONS') and o.name = 'FK_USER_NOT_USER_NOTI_NOTIFICA')
alter table USER_NOTIFICATIONS
   drop constraint FK_USER_NOT_USER_NOTI_NOTIFICA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USER_NOTIFICATIONS') and o.name = 'FK_USER_NOT_USER_NOTI_USERS')
alter table USER_NOTIFICATIONS
   drop constraint FK_USER_NOT_USER_NOTI_USERS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EVENTDONATIONS')
            and   name  = 'EVENTDONATIONS_EVENTPARTICIPATIONS_FK'
            and   indid > 0
            and   indid < 255)
   drop index EVENTDONATIONS.EVENTDONATIONS_EVENTPARTICIPATIONS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EVENTDONATIONS')
            and   type = 'U')
   drop table EVENTDONATIONS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EVENTPARTICIPATIONS')
            and   name  = 'EVENTPARTICIPATIONS_STUDENTS_FK'
            and   indid > 0
            and   indid < 255)
   drop index EVENTPARTICIPATIONS.EVENTPARTICIPATIONS_STUDENTS_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EVENTPARTICIPATIONS')
            and   name  = 'EVENTPARTICIPATIONS_EVENTS_FK'
            and   indid > 0
            and   indid < 255)
   drop index EVENTPARTICIPATIONS.EVENTPARTICIPATIONS_EVENTS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EVENTPARTICIPATIONS')
            and   type = 'U')
   drop table EVENTPARTICIPATIONS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('EVENTS')
            and   name  = 'EVENTS_EVENTTYPES_FK'
            and   indid > 0
            and   indid < 255)
   drop index EVENTS.EVENTS_EVENTTYPES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EVENTS')
            and   type = 'U')
   drop table EVENTS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EVENTTYPES')
            and   type = 'U')
   drop table EVENTTYPES
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('NOTIFICATIONS')
            and   name  = 'NOTIFICATIONS_NOTIFICATIONTYPES_FK'
            and   indid > 0
            and   indid < 255)
   drop index NOTIFICATIONS.NOTIFICATIONS_NOTIFICATIONTYPES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NOTIFICATIONS')
            and   type = 'U')
   drop table NOTIFICATIONS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NOTIFICATIONTYPES')
            and   type = 'U')
   drop table NOTIFICATIONTYPES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ROLES')
            and   type = 'U')
   drop table ROLES
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USERS')
            and   name  = 'USERS_ROLES_FK'
            and   indid > 0
            and   indid < 255)
   drop index USERS.USERS_ROLES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USERS')
            and   type = 'U')
   drop table USERS
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USER_NOTIFICATIONS')
            and   name  = 'USER_NOTIFICATIONS2_FK'
            and   indid > 0
            and   indid < 255)
   drop index USER_NOTIFICATIONS.USER_NOTIFICATIONS2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('USER_NOTIFICATIONS')
            and   name  = 'USER_NOTIFICATIONS_FK'
            and   indid > 0
            and   indid < 255)
   drop index USER_NOTIFICATIONS.USER_NOTIFICATIONS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USER_NOTIFICATIONS')
            and   type = 'U')
   drop table USER_NOTIFICATIONS
go

if exists(select 1 from systypes where name='CREATED_AT')
   drop type CREATED_AT
go

if exists(select 1 from systypes where name='IS_DELETED')
   drop type IS_DELETED
go

if exists(select 1 from systypes where name='UPDATED_AT')
   drop type UPDATED_AT
go

/*==============================================================*/
/* Domain: CREATED_AT                                           */
/*==============================================================*/
create type CREATED_AT
   from datetime
go

/*==============================================================*/
/* Domain: IS_DELETED                                           */
/*==============================================================*/
create type IS_DELETED
   from bit
go

/*==============================================================*/
/* Domain: UPDATED_AT                                           */
/*==============================================================*/
create type UPDATED_AT
   from datetime
go

/*==============================================================*/
/* Table: EVENTDONATIONS                                        */
/*==============================================================*/
CREATE TABLE EVENTDONATIONS (
   EVENTDONATIONS_ID    INT IDENTITY(1,1) NOT NULL,
   PARTICIPATION_ID     INT NOT NULL,
   AMOUNT               INT NULL,
   DONATION_DATE        DATETIME NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_EVENTDONATIONS PRIMARY KEY NONCLUSTERED (EVENTDONATIONS_ID)
)
GO

/*==============================================================*/
/* Table: EVENTPARTICIPATIONS                                   */
/*==============================================================*/
CREATE TABLE EVENTPARTICIPATIONS (
   PARTICIPATION_ID     INT IDENTITY(1,1) NOT NULL,
   EVENTS_ID            INT NOT NULL,
   USER_ID              INT NOT NULL,
   PARTICIPATION_STATUS NVARCHAR(60) NULL,
   EARNED_POINTS        INT NULL,
   PARTICIPATION_TIME   DATETIME NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_EVENTPARTICIPATIONS PRIMARY KEY NONCLUSTERED (PARTICIPATION_ID)
)
GO

/*==============================================================*/
/* Table: EVENTS                                                */
/*==============================================================*/
CREATE TABLE EVENTS (
   EVENTS_ID            INT IDENTITY(1,1) NOT NULL,
   EVENTTYPES_ID        INT NOT NULL,
   EVENT_NAME           NVARCHAR(255) NULL,
   EVENT_TIME           DATETIME NULL,
   END_TIME             DATETIME NULL,
   LOCATION             NVARCHAR(255) NULL,
   DESCRIPTION          NTEXT NULL,
   REQUIRED_PARTICIPANTS INT NULL,
   MAX_PARTICIPANTS     INT NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_EVENTS PRIMARY KEY NONCLUSTERED (EVENTS_ID)
)
GO

/*==============================================================*/
/* Table: EVENTTYPES                                            */
/*==============================================================*/
CREATE TABLE EVENTTYPES (
   EVENTTYPES_ID        INT IDENTITY(1,1) NOT NULL,
   EVENTTYPES_NAME      NVARCHAR(1000) NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_EVENTTYPES PRIMARY KEY NONCLUSTERED (EVENTTYPES_ID)
)
GO

/*==============================================================*/
/* Table: NOTIFICATIONS                                         */
/*==============================================================*/
CREATE TABLE NOTIFICATIONS (
   NOTIFICATIONS_ID     INT IDENTITY(1,1) NOT NULL,
   NOTIFICATIONTYPES_ID INT NOT NULL,
   TITLE                NVARCHAR(255) NULL,
   MESSAGE              NTEXT NULL,
   STATUS               NVARCHAR(60) NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_NOTIFICATIONS PRIMARY KEY NONCLUSTERED (NOTIFICATIONS_ID)
)
GO

/*==============================================================*/
/* Table: NOTIFICATIONTYPES                                     */
/*==============================================================*/
CREATE TABLE NOTIFICATIONTYPES (
   NOTIFICATIONTYPES_ID INT IDENTITY(1,1) NOT NULL,
   NAME                 NVARCHAR(255) NULL,
   DESCRIPTION          NTEXT NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_NOTIFICATIONTYPES PRIMARY KEY NONCLUSTERED (NOTIFICATIONTYPES_ID)
)
GO

/*==============================================================*/
/* Table: ROLES                                                 */
/*==============================================================*/
CREATE TABLE ROLES (
   ROLES_ID             INT IDENTITY(1,1) NOT NULL,
   NAME                 NVARCHAR(255) NULL,
   DESCRIPTION          NTEXT NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_ROLES PRIMARY KEY NONCLUSTERED (ROLES_ID)
)
GO

/*==============================================================*/
/* Table: USERS                                                 */
/*==============================================================*/
CREATE TABLE USERS (
   USER_ID              INT IDENTITY(1,1) NOT NULL,
   ROLES_ID             INT NOT NULL,
   STUDENT_ID           NVARCHAR(20) NULL,
   FULL_NAME            NVARCHAR(255) NULL,
   CLASSID              NVARCHAR(12) NULL,
   CLASSNAME            NVARCHAR(255) NULL,
   EMAIL                NVARCHAR(255) NULL,
   PHONE_NUMBER         NVARCHAR(20) NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_USERS PRIMARY KEY NONCLUSTERED (USER_ID)
)
GO

/*==============================================================*/
/* Table: USER_NOTIFICATIONS                                    */
/*==============================================================*/
CREATE TABLE USER_NOTIFICATIONS (
   NOTIFICATIONS_ID     INT NOT NULL,
   USER_ID              INT NOT NULL,
   STATUS               NVARCHAR(60) NULL,
   CREATE_AT            DATETIME NULL,
   UPDATE_AT            DATETIME NULL,
   IS_DELETE            BIT NULL,
   CONSTRAINT PK_USER_NOTIFICATIONS PRIMARY KEY (NOTIFICATIONS_ID, USER_ID)
)
GO

/*==============================================================*/
/* Index: USER_NOTIFICATIONS_FK                                 */
/*==============================================================*/
create index USER_NOTIFICATIONS_FK on USER_NOTIFICATIONS (
NOTIFICATIONS_ID ASC
)
go

/*==============================================================*/
/* Index: USER_NOTIFICATIONS2_FK                                */
/*==============================================================*/
create index USER_NOTIFICATIONS2_FK on USER_NOTIFICATIONS (
USER_ID ASC
)
go

alter table EVENTDONATIONS
   add constraint FK_EVENTDON_EVENTDONA_EVENTPAR foreign key (PARTICIPATION_ID)
      references EVENTPARTICIPATIONS (PARTICIPATION_ID)
go

alter table EVENTPARTICIPATIONS
   add constraint FK_EVENTPAR_EVENTPART_EVENTS foreign key (EVENTS_ID)
      references EVENTS (EVENTS_ID)
go

alter table EVENTPARTICIPATIONS
   add constraint FK_EVENTPAR_EVENTPART_USERS foreign key (USER_ID)
      references USERS (USER_ID)
go

alter table EVENTS
   add constraint FK_EVENTS_EVENTS_EV_EVENTTYP foreign key (EVENTTYPES_ID)
      references EVENTTYPES (EVENTTYPES_ID)
go

alter table NOTIFICATIONS
   add constraint FK_NOTIFICA_NOTIFICAT_NOTIFICA foreign key (NOTIFICATIONTYPES_ID)
      references NOTIFICATIONTYPES (NOTIFICATIONTYPES_ID)
go

alter table USERS
   add constraint FK_USERS_USERS_ROL_ROLES foreign key (ROLES_ID)
      references ROLES (ROLES_ID)
go

alter table USER_NOTIFICATIONS
   add constraint FK_USER_NOT_USER_NOTI_NOTIFICA foreign key (NOTIFICATIONS_ID)
      references NOTIFICATIONS (NOTIFICATIONS_ID)
go

alter table USER_NOTIFICATIONS
   add constraint FK_USER_NOT_USER_NOTI_USERS foreign key (USER_ID)
      references USERS (USER_ID)
go

