create table Student( --学生表
StuID int primary key identity(1,1), --学生ID
StuNumber nvarchar(50), --学生学号
StuName nvarchar(50), --学生姓名
StuCount nvarchar(50), --学生登录账号
StuPwd nvarchar(50), --学生登录密码
StuSex int,--0为女1为男学生性别 --学生性别
StuAge int, --学生年龄
OccupancyStatus int, --入住状态（0未入住1已入住2已迁出）
IsDelete int --是否删除（0未删除1已删除）
)
update Student set  OccupancyStatus=1 where StuID='1'
insert into Student values('201817380104','陈柏鹏','201817380104','12345',1,19,0,0)
create table Admin( --管理员表
AdminID int primary key identity(1,1), --管理员ID
FloorID int foreign key references Floor(FloorID), --楼宇ID
AdminCount nvarchar(50), --管理员登录账号
AdminPwd nvarchar(50), --管理员登录密码
AdminSex int,--0为女1为男 --管理员性别
AdminAge int, --管理员年龄
AdminKinds int, --管理员种类（0楼宇管理员1系统管理员2学生）
IsDelete int, --是否删除（0未删除1已删除）
AdminName nvarchar(50)--管理员姓名
)
alter table Admin add AdminName nvarchar(50)
select * from Admin
select * from Student
insert into Admin(AdminCount,AdminPwd,AdminSex,AdminAge,AdminKinds,IsDelete) values('admin','1234',1,20,1,0)
insert into Admin values(1,'余罪','1234',1,28,0,0)
create table Floor( --楼宇表
FloorID int primary key identity(1,1), --楼宇ID
FloorName nvarchar(50), --楼宇名称
FloorTime date, --楼宇创建时间
FloorMark nvarchar(50), --备注
IsDelete int --是否删除（0未删除1已删除）
)
select * from Floor
insert into Floor values('翠竹苑A栋','2000-11-15','用于学生的入住',0)
create table Dorm( --宿舍表
DormID int primary key identity(1,1), --宿舍ID
FloorID int foreign key references Floor(FloorID), --楼宇ID
DormName nvarchar(50), --宿舍名称
DormPeople int, --宿舍人数
IsDelete int --是否删除（0未删除1已删除）
)
select * from Dorm
insert into Dorm values(1,'201',6,0)
create table Moveinto( --入住登记表
MoveintoID int primary key identity(1,1),--入住登记ID
StuID int foreign key references Student(StuID), --学生ID
FloorID int foreign key references Floor(FloorID), --楼宇ID
DormID int foreign key references Dorm(DormID), --宿舍ID
MoveintoTime date, --入住登记时间
MoveintoPeople nvarchar(50), --入住登记人
IsDelete int --是否删除（0未删除1已删除）
)
select * from Moveinto
insert into Moveinto values(1,1,1,'2019-1-8','翠竹苑A栋宿管',0)
create table Exchange( --宿舍调换表
ExchangeID int, --调换ID
StuID int, --学生ID
OldFloorID int, --原楼宇ID
OldDormID int, --原宿舍ID
NewFloorID int, --新楼宇ID
NewDormID int --新宿舍ID
)
create table Moveout( --迁出表
MoveoutID int, --迁出ID
StuID int, --学生ID
MoveoutDate date, --迁出时间
MoveoutMark nvarchar(50), --备注
IsDelete int --是否删除（0未删除1已删除）
)
create table Attendance( --考勤表
AttendanceID int primary key identity(1,1), --考勤ID
StuID int foreign key references Student(StuID), --学生ID
AttendanceDate date, --晚归日期
AttendanceReason nvarchar(50) --晚归原因
)
insert into Attendance values(1,'2020-6-15','忘记时间')