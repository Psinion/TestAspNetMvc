CREATE DATABASE tIntegrationDb;
GO

USE tIntegrationDb;

CREATE TABLE PC 
(
	Id INT PRIMARY KEY IDENTITY,
	Cpu FLOAT,
	Memory FLOAT,
	Hdd FLOAT
);

CREATE TABLE Departments
(
	Id INT PRIMARY KEY IDENTITY,
	Name VARCHAR(50)
);

CREATE TABLE Users
(
	Id INT PRIMARY KEY IDENTITY,
	UserName VARCHAR(50),
	Salary FLOAT,
	DepartmentId INT,
	PCId INT,
	FOREIGN KEY (DepartmentId) 	REFERENCES Departments (Id),
	FOREIGN KEY (PCId) 			REFERENCES PC (Id)
);

CREATE TABLE AddressParts
(
	Id INT PRIMARY KEY IDENTITY,
	ParentId INT,
	AddrPartTypeId INT,
	Name VARCHAR(50),
	FOREIGN KEY (ParentId) REFERENCES AddressParts (Id),
);

SET IDENTITY_INSERT PC ON; 
INSERT PC (Id, Cpu, Memory, Hdd)
VALUES 
(1	, 3200	, 8000	, 500),
(2	, 3300	, 8000	, 500),
(3	, 3300	, 12000	, 500),
(4	, 3100	, 12000	, 1000),
(5	, 4200	, 16000	, 2000),
(6	, 3800	, 32000	, 1500),
(7	, 3200	, 4000	, 1500),
(8	, 3200	, 4000	, 1500),
(9	, 3200	, 8000	, 2000),
(10	, 2800	, 3000	, 120),
(11	, 3500	, 3000	, 450),
(12	, 2500	, 3000	, 600),
(13	, 2500	, 2000	, 120),
(14	, 3200	, 500	, 400),
(15	, 3500	, 800	, 1000),
(16	, 3200	, 400	, 700);
SET IDENTITY_INSERT PC OFF; 

SET IDENTITY_INSERT Departments ON;
INSERT Departments (Id, Name)
VALUES 
(1	, 'Отдел кадров'),
(2	, 'Бухгалтерия'),
(3	, 'Отдел охраны труда'),
(4	, 'Служба безопасности'),
(5	, 'Отдел IT'),
(6	, 'Аналитический отдел'),
(7	, 'Технологический отдел'),
(8	, 'Транспортный отдел');
SET IDENTITY_INSERT Departments OFF;

INSERT Users (UserName, Salary, DepartmentId, PCId)
VALUES 
('Антон'		, 140000	, 5	, 6),
('Михаил'		, 95000		, 6	, 1),
('Наталья'		, 93000		, 7	, 2),
('Александра'	, 85000		, 1	, 3),
('Петр'			, 21000		, 3	, 4),
('Алексей'		, 109000	, 4	, 5),
('Николай'		, 43000		, 2	, 7),
('Олег'			, 121000	, 3	, 8),
('Дмитрий'		, 128000	, 5	, 9),
('Роман'		, 106000	, 6	, 10),
('Денис'		, 147000	, 7	, 11),
('Никита'		, 72500		, 8	, 12),
('Максим'		, 29000		, 1	, 13),
('Светлана'		, 89000		, 2	, 15),
('Кристина'		, 79000		, 3	, 16);

SET IDENTITY_INSERT AddressParts ON;
INSERT AddressParts (Id, ParentId, AddrPartTypeId, Name)
VALUES 
(1, 	NULL, 1	, 'Россия'),
(2, 	1	, 2	, 'Московская обл.'),
(3, 	2	, 3	, 'р-н Мытищенский'),
(4, 	3	, 4	, 'г. Мытищи'),
(5, 	4	, 5	, 'ул. Ульяновская'),
(6, 	5	, 6	, 'д. 3'),
(7, 	2	, 3	, 'р-н Можайский'),
(8, 	7	, 4	, 'г. Можайск'),
(9, 	8	, 5	, 'ул. 1-я Слобода'),
(10, 	9	, 6	, 'д. 17');
SET IDENTITY_INSERT AddressParts OFF;