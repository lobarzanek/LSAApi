insert into dbo.Roles values
('root'),
('admin'),
('user')

select * from dbo.Roles

insert into dbo.Producents values
('Extreme'),
('Cisco'),
('Arista')

select * from dbo.Producents

insert into dbo.Models values
('5320-16P-4XE', 16, 1),
('5320-24T-8XE', 24, 1)

select * from dbo.Models

insert into dbo.SwitchStatuses values
('Active'),
('Offline')

select * from dbo.SwitchStatuses

insert into dbo.Sections values
('Section-A'),
('Section-B'),
('Section-C'),
('Section-D')

select * from dbo.Sections

insert into dbo.Vlans values
('v10-10.10.10.0', 'v10', '10.10.10.0'),
('v20-v10.10.20.0', 'v20', '10.10.20.0'),
('v30-v10.10.30.0', 'v30', '10.10.30.0')

select * from dbo.Vlans

insert into dbo.Users values
('root1', 'login1', 'pass1', 1),
('admin2', 'login2', 'pass2', 2),
('user3', 'login3', 'login3', 3),
('user4', 'login4', 'login4', 3),
('user5', 'login5', 'login5', 3)

select * from dbo.Users

insert into dbo.ConfigStatuses values
( 'Pending'),
('InProgress'),
('Done'),
('Error'),
('Unknown')

select * from dbo.ConfigStatuses


insert into dbo.Switches values
('A-Switch1', '10.10.10.1', 'login1', 'pass1', 'www.netbox1.com', 1, 2, 1),
('A-Switch2', '10.10.10.2', 'login2', 'pass2', 'www.netbox2.com', 1, 1, 1),
('B-Switch1', '10.10.10.3', 'login3', 'pass3', 'www.netbox3.com', 2, 2, 2)

select * from dbo.Switches

insert into dbo.Configurations values
('2023-06-23 07:30:20',1,3,3),
('2023-06-24 09:30:20',1,1,3),
('2023-06-25 11:30:20',2,4,4)

select * from dbo.Configurations



