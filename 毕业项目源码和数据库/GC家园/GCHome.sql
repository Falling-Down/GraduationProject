create table Student( --ѧ����
StuID int primary key identity(1,1), --ѧ��ID
StuNumber nvarchar(50), --ѧ��ѧ��
StuName nvarchar(50), --ѧ������
StuCount nvarchar(50), --ѧ����¼�˺�
StuPwd nvarchar(50), --ѧ����¼����
StuSex nvarchar(2), --ѧ���Ա�
StuAge nvarchar(2), --ѧ������
OccupancyStatus int, --��ס״̬��0δ��ס1����ס2��Ǩ����
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
create table Admin( --����Ա��
AdminID int, --����ԱID
FloorID int, --¥��ID
AdminCount nvarchar(50), --����Ա��¼�˺�
AdminPwd nvarchar(50), --����Ա��¼����
AdminSex nvarchar(2), --����Ա�Ա�
AdminAge nvarchar(2), --����Ա����
AdminKinds int, --����Ա���ࣨ0¥�����Ա1ϵͳ����Ա��
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
create table Floor( --¥���
FloorID int, --¥��ID
FloorName nvarchar(50), --¥������
FloorTime date, --¥���ʱ��
FloorMark nvarchar(50), --��ע
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
create table Dorm( --�����
DormID int, --����ID
FloorID int, --¥��ID
DormName nvarchar(50), --��������
DormPeople int, --��������
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
create table Moveinto( --��ס�ǼǱ�
MoveintoID int,--��ס�Ǽ�ID
StuID int, --ѧ��ID
FloorID int, --¥��ID
DormID int, --����ID
MoveintoTime date, --��ס�Ǽ�ʱ��
MoveintoPeople nvarchar(50), --��ס�Ǽ���
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
create table Exchange( --���������
ExchangeID int, --����ID
StuID int, --ѧ��ID
OldFloorID int, --ԭ¥��ID
OldDormID int, --ԭ����ID
NewFloorID int, --��¥��ID
NewDormID int --������ID
)
create table Moveout( --Ǩ����
MoveoutID int, --Ǩ��ID
StuID int, --ѧ��ID
MoveoutDate date, --Ǩ��ʱ��
MoveoutMark nvarchar(50), --��ע
IsDelete int --�Ƿ�ɾ����0δɾ��1��ɾ����
)
create table Attendance( --���ڱ�
AttendanceID int, --����ID
StuID int, --ѧ��ID
AttendanceDate date, --�������
AttendanceReason nvarchar(50) --���ԭ��
)