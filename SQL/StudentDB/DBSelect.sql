USE [Student_dudnic]
GO
-- Task 1
ALTER TABLE dbo.Student
ADD FOREIGN KEY (StudyTypeId) REFERENCES StudyType(Id);
GO

-- Task 2
DECLARE @AddressId INT, @GroupId INT
SET @AddressId = (SELECT Id from dbo.Address WHERE Name = '36832 Evergreen Hill') -- Address of a hostel
SET @GroupId = (SELECT Id FROM StudentGroup WHERE Name = 'KM') -- Group Name


SELECT CONCAT(RTRIM(LastName), ' ', LEFT(FirstName, 1), '.', LEFT(MiddleName, 1), '.') AS Person, Address.Name AS Address FROM Person
INNER JOIN Student ON Student.PersonId = Person.Id
INNER JOIN Address ON Person.AddressId = Address.Id
WHERE Address.Id <> @AddressId AND EXISTS(
	SELECT * FROM StudentGroupMember WHERE GroupId = @GroupId AND StudentId = Student.Id

)
GO

-- Task 3
DECLARE @GenderId INT
SET @GenderId = (SELECT Id from dbo.Gender WHERE GenderValue = 'male') -- Male


SELECT	CONCAT(RTRIM(LastName), ' ', LEFT(FirstName, 1), '.', LEFT(MiddleName, 1), '.') AS Person,
		Gender,
		BirthDate,
		FLOOR(DATEDIFF(DAY, BirthDate, SYSDATETIME()) / 365.25) AS Age,
		IsMilitaryObligatory = 1
FROM Person
INNER JOIN Student ON Student.PersonId = Person.Id
WHERE Gender = @GenderId 
	AND (FLOOR(DATEDIFF(DAY, BirthDate, SYSDATETIME()) / 365.25) BETWEEN 18 AND 27)
	AND IsMilitaryObligatory = 1

GO

-- Task 4
DECLARE @AddressId INT, @GroupIdKM INT, @GroupIdKC INT
SET @AddressId = (SELECT Id from dbo.Address WHERE Name = '36832 Evergreen Hill') -- Address of a hostel
SET @GroupIdKM = (SELECT Id FROM StudentGroup WHERE Name = 'KM') -- Group Names
SET @GroupIdKC = (SELECT Id FROM StudentGroup WHERE Name='KC') -- Group Names


SELECT CONCAT(RTRIM(LastName), ' ', LEFT(FirstName, 1), '.', LEFT(MiddleName, 1), '.') AS Person, Address.Name AS Address FROM Person
INNER JOIN Student ON Student.PersonId = Person.Id
INNER JOIN Address ON Person.AddressId = Address.Id
WHERE Address.Id = @AddressId AND EXISTS(
	SELECT * FROM StudentGroupMember WHERE (GroupId = @GroupIdKM OR GroupId = @GroupIdKC) AND StudentId = Student.Id
)
GO

-- Task 5
SELECT	CONCAT(RTRIM(LastName), ' ', LEFT(FirstName, 1), '.', LEFT(MiddleName, 1), '.') AS Person,
		CourseNumber, 
		ScientificWork.Name AS Name,
		ScientificType.Name AS Type
FROM Person
INNER JOIN Student ON Student.PersonId = Person.Id
INNER JOIN ScientificWorkAuthor ON ScientificWorkAuthor.PersonId = Person.Id
INNER JOIN ScientificWork ON ScientificWork.Id = ScientificWorkAuthor.ScientificWorkId
INNER JOIN ScientificType ON ScientificType.Id = ScientificWork.ScientificTypeId


-- Task 6
DECLARE @StudentId INT
SET @StudentId = 2

SELECT	CONCAT(RTRIM(LastName), ' ', LEFT(FirstName, 1), '.', LEFT(MiddleName, 1), '.') AS Person,
		Subject.Name AS Subject,
		Mark
FROM Person
INNER JOIN Student ON Student.PersonId = Person.Id
INNER JOIN SubjectMark ON SubjectMark.StudentId = Student.Id
INNER JOIN Subject ON SubjectMark.SubjectId = Subject.Id
WHERE Student.Id = @StudentId
GO

-- Task 7
WITH SubjectsView AS(
SELECT * FROM Subject
WHERE SpecialityNumber = 122 OR SpecialityNumber = 124
)
SELECT Name, CourseNumber FROM SubjectsView