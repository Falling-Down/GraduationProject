create database GCHome
use GCHome
create table Student( --ѧ����
StuID int primary key identity(1,1), --ѧ��ID
StuNumber nvarchar(50), --ѧ��ѧ��
StuName nvarchar(50), --ѧ������
StuCount nvarchar(50), --ѧ����¼�˺�
StuPwd nvarchar(50), --ѧ����¼����
StuSex int,--0ΪŮ1Ϊ��ѧ���Ա� --ѧ���Ա�
StuAge int, --ѧ������
OccupancyStatus int, --��ס״̬��0δ��ס1����ס2��Ǩ����
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
update Student set  OccupancyStatus=1 where StuID='1'
insert into Student values('201817380104','�°���','201817380104','12345',1,19,0,0)
create table Admin( --����Ա��
AdminID int primary key identity(1,1), --����ԱID
FloorID int foreign key references Floor(FloorID), --¥��ID
AdminCount nvarchar(50), --����Ա��¼�˺�
AdminPwd nvarchar(50), --����Ա��¼����
AdminName nvarchar(50),--����Ա����
AdminSex int,--0ΪŮ1Ϊ�� --����Ա�Ա�
AdminAge int, --����Ա����
AdminKinds int, --����Ա���ࣨ0¥�����Ա1ϵͳ����Ա2ѧ����
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
select * from Admin
select * from Student
insert into Admin(AdminCount,AdminPwd,AdminName,AdminSex,AdminAge,AdminKinds,IsDelete) values('admin','1234','admin',1,20,1,0)
insert into Admin values(1,'����','1234',1,28,0,0)
create table Floor( --¥���
FloorID int primary key identity(1,1), --¥��ID
FloorName nvarchar(50), --¥������
FloorTime date, --¥���ʱ��
FloorMark nvarchar(50), --��ע
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
select * from Floor
insert into Floor values('����ԷA��','2000-11-15','����ѧ������ס',0)
create table Dorm( --�����
DormID int primary key identity(1,1), --����ID
FloorID int foreign key references Floor(FloorID), --¥��ID
DormName nvarchar(50), --��������
DormPeople int, --��������
MoveinDormPeople int,--����ס����
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
select * from Exchange
select * from Dorm
insert into Dorm values(1,'201',6,0)
create table Moveinto( --��ס�ǼǱ�
MoveintoID int primary key identity(1,1),--��ס�Ǽ�ID
StuID int foreign key references Student(StuID), --ѧ��ID
FloorID int foreign key references Floor(FloorID), --¥��ID
DormID int foreign key references Dorm(DormID), --����ID
MoveintoTime date, --��ס�Ǽ�ʱ��
MoveintoPeople nvarchar(50), --��ס�Ǽ���
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
select * from Moveinto
select * from Exchange
insert into Moveinto values(1,1,1,'2019-1-8','����ԷA���޹�',0)
create table Exchange( --���������
ExchangeID int primary key identity(1,1), --����ID
StuID int foreign key references Student(StuID), --ѧ��ID
OldFloorID int foreign key references Floor(FloorID), --ԭ¥��ID
OldDormID int foreign key references Dorm(DormID), --ԭ����ID
NewFloorID int foreign key references Floor(FloorID), --��¥��ID
NewDormID int foreign key references Dorm(DormID), --������ID
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
select * from Exchange
create table Moveout( --Ǩ����
MoveoutID int primary key identity(1,1), --Ǩ��ID
StuID int foreign key references Student(StuID), --ѧ��ID
MoveoutDate date, --Ǩ��ʱ��
MoveoutMark nvarchar(50), --��ע
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
select * from Moveout
insert into Moveout values(1,'2020-6-11','���ж���������',0)
create table Attendance( --���ڱ�
AttendanceID int primary key identity(1,1), --����ID
StuID int foreign key references Student(StuID), --ѧ��ID
AttendanceDate date, --�������
AttendanceReason nvarchar(50), --���ԭ��
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
select * from attendance
alter table Attendance add IsDelete int
insert into Attendance values(1,'2020-6-15','����ʱ��')
create table Fix( --�����
FixID int primary key identity(1,1), --����ID
StuID int foreign key references Student(StuID),--ѧ��ID
Adress nvarchar(50),--��ַ
Phone nvarchar(20),--��ϵ��ʽ
FixReason nvarchar(100),--�ϱ�ԭ��
FixDate date,--�ϱ�ʱ��
XsReason nvarchar(100),--��������
XsDate date,--����ʱ��
FixState int,--����״̬(0δ����1���ڴ���2������3���δ���)
IsDelete int --�Ƿ�ɾ��(0δɾ��1��ɾ��)
)
alter table fix add XsDate date
insert into Fix values(12,'����ԷC�� 509','13397356317','���ҵķ��Ȼ���','',0,0)
select * from Fix