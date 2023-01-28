-- 1.1.	Написать запрос, который сформирует выборку тактовых частот процессоров  компьютеров (cpu) у которых объем памяти равен 3000Mb. 

SELECT PC.Cpu
FROM PC
WHERE PC.Memory = 3000;

-- 1.2. Написать запрос, который сформирует выборку пользователей, компьютер  которых содержит жесткий диск объемом > 500Gb. 
-- Выборка так же должна содержать отдел, в котором работает пользователь.

SELECT 
	  U.UserName
	, U.Salary
	, D.Name
FROM USERS U
JOIN PC ON PC.ID = U.PCId
JOIN Departments D ON D.Id = U.DepartmentId
WHERE PC.Hdd > 500

-- 1.3.	Написать запрос, который сформирует выборку отделов и количества сотрудников, работающих в этих отделах.  
-- Вывести наименование отдела и кол. сотрудников данного отдела.

SELECT D.Name, COUNT(U.Id) as EmployeesNumber
FROM Departments D
JOIN Users U ON U.DepartmentId = D.Id
GROUP BY D.Name

-- 1.4.	Написать запрос, который сформирует выборку отделов и количества сотрудников, работающих в этих отделах.  
-- Вывести наименование отдела и кол. сотрудников данного отдела.

SELECT D.Name, COUNT(U.Id) as EmployeesNumber, SUM(U.Salary) as SalariesSum
FROM Departments D
JOIN Users U ON U.DepartmentId = D.Id
WHERE U.Salary > 100000
GROUP BY D.Name

-- 1.5. Написать запрос, который сформирует выборку компьютеров отдела, у сотрудников которого максимальная сумма окладов.

SELECT PC.*, D.Name AS DepartmentName
FROM PC
JOIN Users U ON U.PCId = PC.Id
JOIN Departments D ON D.Id = U.DepartmentId
WHERE U.DepartmentId = (SELECT RichestDep.Id
						FROM (SELECT TOP(1) U.DepartmentId AS Id, Sum(U.Salary) as SalariesSum
							  FROM Users U
							  GROUP BY U.DepartmentId
							  ORDER BY SalariesSum DESC) RichestDep
						);
						
-- 1.6.	Написать функцию, которая по заданному параметру AddrPartId сформирует на выходе строку,
-- содержащую полный адрес по всем родительским адресообразующим элементам.

DECLARE @AddrPartId INT;
SET @AddrPartId = 2;

WITH AddressRec(Id, ParentId, Name, Depth)
AS
(
    SELECT Ap.Id, Ap.ParentId, AP.Name, 1 as Depth
    FROM AddressParts AP
    WHERE AP.Id = @AddrPartId

    UNION ALL

    SELECT Ap.Id, Ap.ParentId, AP.Name, AR.Depth + 1 as Depth
    FROM AddressParts AP
	JOIN AddressRec AR ON AP.Id = AR.ParentId
)
SELECT STRING_AGG(AR.Name, ', ') WITHIN GROUP (ORDER BY AR.DEPTH DESC)
FROM AddressRec AR