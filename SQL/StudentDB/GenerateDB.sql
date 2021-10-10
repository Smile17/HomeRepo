USE [Student_dudnic]
GO
-- ------------------------------------------Drop all foreign keys from the database
declare @sql nvarchar(max) = (
    SELECT 
        'alter table ' + quotename(schema_name(schema_id)) + '.' +
        quotename(object_name(parent_object_id)) +
        ' drop constraint '+quotename(name) + ';'
    FROM sys.foreign_keys
    for xml path('')
);
exec sp_executesql @sql;

-- -------------------------------------------Drop all tables from the database
DECLARE @sql_table NVARCHAR(max)=''

SELECT @sql_table += ' Drop table ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
FROM   INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'

exec sp_executesql @sql_table

-- -------------------------------------------Drop a pecific table from the database
IF OBJECT_ID('dbo.Gender', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Gender]

CREATE TABLE [dbo].[Gender]
(
   [Id] BIT NOT NULL PRIMARY KEY,
   [GenderValue] NCHAR(6) NOT NULL
);
GO

IF OBJECT_ID('dbo.Address', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Address]

CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(40) NULL
)

IF OBJECT_ID('dbo.Person', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Person]

CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NCHAR(20) NULL, 
    [LastName] NCHAR(20) NULL, 
    [MiddleName] NCHAR(20) NULL, 
    [Gender] BIT NOT NULL, 
    [BirthDate] DATE NULL, 
    [AddressId] INT NULL, 
    [Phone] CHAR(15) NULL,
	CONSTRAINT FK_Gender FOREIGN KEY ([Gender])
        REFERENCES Gender (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_Address FOREIGN KEY ([AddressId])
        REFERENCES Address (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
)
GO
IF OBJECT_ID('dbo.Student', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Student]

IF OBJECT_ID('dbo.StudyType', 'u') IS NOT NULL 
  DROP TABLE [dbo].[StudyType]

CREATE TABLE [dbo].[StudyType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NCHAR(20)
)
GO

CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[PersonId] int NOT NULL REFERENCES Person,
    [IsContract] BIT NULL, 
    [StudyTypeId] INT NULL REFERENCES StudyType, 
    [IsMilitaryObligatory] BIT NULL,
	[CourseNumber] INT
)
GO

IF OBJECT_ID('dbo.StudentGroup', 'u') IS NOT NULL 
  DROP TABLE [dbo].[StudentGroup]


  CREATE TABLE [dbo].[StudentGroup]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] NCHAR(10) NULL
)
GO
CREATE TABLE [dbo].[StudentGroupMember]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[GroupId] INT NOT NULL,
    [StudentId] INT NULL,
	CONSTRAINT FK_StudentId FOREIGN KEY ([StudentId])
        REFERENCES Student (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_GroupId FOREIGN KEY ([GroupId])
        REFERENCES StudentGroup (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
)
GO

IF OBJECT_ID('dbo.Subject', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Subject]
CREATE TABLE [dbo].[Subject]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NCHAR(30) NULL,
	[SpecialityNumber] INT,
	[CourseNumber] INT
)
GO

IF OBJECT_ID('dbo.SubjectMark', 'u') IS NOT NULL 
  DROP TABLE [dbo].[SubjectMark]

CREATE TABLE [dbo].[SubjectMark]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[StudentId] INT NULL, 
    [SubjectId] INT NULL, 
    [Mark] INT NULL,
	CONSTRAINT FK_StudentId_SubjectMark FOREIGN KEY ([StudentId])
        REFERENCES Student (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_SubjectId FOREIGN KEY ([SubjectId])
        REFERENCES Subject (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
)


IF OBJECT_ID('dbo.AcademicDegree', 'u') IS NOT NULL 
  DROP TABLE [dbo].[AcademicDegree]
CREATE TABLE [dbo].[AcademicDegree]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NCHAR(30) NULL
)
GO

IF OBJECT_ID('dbo.Degree', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Degree]
CREATE TABLE [dbo].[Degree]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NCHAR(30) NULL
)
GO



IF OBJECT_ID('dbo.Teacher', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Teacher]

CREATE TABLE [dbo].[Teacher]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[PersonId] INT NOT NULL,
    [AcademicDegree] INT NULL, 
    [Degree] INT NULL,
    CONSTRAINT FK_RefRecId_Teacher FOREIGN KEY ([PersonId])
        REFERENCES Person (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_AcademicDegree_Teacher FOREIGN KEY ([AcademicDegree])
        REFERENCES AcademicDegree (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_DegreeTeacher FOREIGN KEY ([Degree])
        REFERENCES Degree (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
)
GO

IF OBJECT_ID('dbo.Position', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Position]
CREATE TABLE [dbo].[Position]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NCHAR(30) NULL
)
GO

IF OBJECT_ID('dbo.Faculty', 'u') IS NOT NULL 
  DROP TABLE [dbo].[Faculty]
CREATE TABLE [dbo].[Faculty]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NCHAR(30) NULL
)
GO

IF OBJECT_ID('dbo.TeacherPosition', 'u') IS NOT NULL 
  DROP TABLE [dbo].[TeacherPosition]
CREATE TABLE [dbo].[TeacherPosition]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TeacherId] INT NOT NULL,
    [PositionId] INT NULL, 
    [FacultyId] INT NULL,
    CONSTRAINT FK_TeacherId FOREIGN KEY ([TeacherId])
        REFERENCES Teacher (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_PositionId FOREIGN KEY ([PositionId])
        REFERENCES Position (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_FacultyId FOREIGN KEY ([FacultyId])
        REFERENCES Faculty (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
)
GO

IF OBJECT_ID('dbo.ScientificPublisher', 'u') IS NOT NULL 
  DROP TABLE [dbo].[ScientificPublisher]
CREATE TABLE [dbo].[ScientificPublisher]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NCHAR(30) NULL
)
GO

IF OBJECT_ID('dbo.ScientificWork', 'u') IS NOT NULL 
  DROP TABLE [dbo].[ScientificWork]

CREATE TABLE [dbo].[ScientificType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NCHAR(30) NULL
)
GO

CREATE TABLE [dbo].[ScientificWork]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NCHAR(50) NOT NULL,
	[ScientificTypeId] INT NOT NULL REFERENCES ScientificType,
    [PublisherId] INT NULL, 
    CONSTRAINT FK_PublisherId FOREIGN KEY ([PublisherId])
        REFERENCES ScientificPublisher (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
	
)
GO
IF OBJECT_ID('dbo.ScientificWorkAuthor', 'u') IS NOT NULL 
  DROP TABLE [dbo].[ScientificWorkAuthor]
CREATE TABLE [dbo].[ScientificWorkAuthor]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ScientificWorkId] INT NOT NULL, 
    [PersonId] INT NOT NULL, 
    CONSTRAINT FK_ScientificWorkId FOREIGN KEY ([ScientificWorkId])
        REFERENCES ScientificWork (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	CONSTRAINT FK_PersonId_ScientificWorkAuthor FOREIGN KEY ([PersonId])
        REFERENCES Person (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
	
)
GO





