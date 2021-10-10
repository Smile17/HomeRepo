USE [Student_dudnic]
GO

INSERT INTO [dbo].[Gender]
           ([Id]
           ,[GenderValue])
     VALUES
           (0, 'male'),
           (1, 'female')
GO

INSERT INTO [dbo].[Subject]
     VALUES
           ('Calculus',122,1),
		   ('Linear algebra',122,1),
		   ('Database',122,3),
		   ('General programming',122,1),
		   ('Object Oriented Programming',122,2),
		   ('General Math',122,1),
		   ('Physics',122,1),
		   ('English',122,1),
		   ('Ukranian',122,1),
		   ('Russian',122,1),
		   ('History of Ukraine',122,2),
		   ('Philosophy',122,1),
		   ('Economics',120,1),
		   ('Biology',110,2),
		   ('Chemistry',110,3),
		   ('Discrete Mathematics',122,1),
		   ('Machine Learning',122,4),
		   ('Data Analysis',124,4),
		   ('Management',123,1),
		   ('Psycology',122,3),
		   ('Risk management',122,4),
		   ('Deep learning',122,4)
GO

insert into dbo.Address (name) values ('58712 Hagan Pass');
insert into dbo.Address (name) values ('36 Dayton Crossing');
insert into dbo.Address (name) values ('3968 Becker Lane');
insert into dbo.Address (name) values ('1 Pearson Way');
insert into dbo.Address (name) values ('6 Sutherland Place');
insert into dbo.Address (name) values ('315 Crownhardt Trail');
insert into dbo.Address (name) values ('2 Colorado Terrace');
insert into dbo.Address (name) values ('99843 Mendota Court');
insert into dbo.Address (name) values ('36832 Evergreen Hill');
insert into dbo.Address (name) values ('33 Northland Terrace');


INSERT INTO [dbo].[Person]
           ([FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[Gender]
           ,[BirthDate]
           ,[AddressId]
           ,[Phone])
     VALUES
           ('Vasyliy', 'Ivanov', 'Yuriyovuch', 0, '10/12/1998', 1, '+38099123343'),
		   ('Ignat', 'Ivanov', 'Yuriyovuch', 0, '10/12/1998', 2, '+38099123343'),
		   ('Kateryna', 'Stepanchuk', 'Ivanivna', 1, '01/08/1997', 3, '+38066234934'),
		   ('Kateryna', 'Bondar', 'Mykhailovna', 1, '01/06/2000', 2, '+38066224233'),
		   ('Iryna', 'Fyinman', 'Ivanivna', 1, '01/08/1997', 3, '+3804334934'),
		   ('Olgha', 'Moroz', 'Sergiyivna', 1, '05/02/2001', 3, '+380423434234'),
		   ('Vasyliy', 'Moroz', 'Yuriyovuch', 0, '10/12/1998', 5, '+38099123343'),
		   ('Ivan', 'Bondarenko', 'Ivanovuch', 0, '10/11/1995', 4, '+38099123243'),
		   ('Sergiyi', 'Ivanov', 'Stepanovuch', 0, '10/12/1998', 3, '+380955094323'),
		   ('Sergiyi', 'Bondarenko', 'Yuriyovuch', 0, '10/09/1990', 5, '+3809934243'),
		   ('Sergiyi', 'Voron', 'Ivanovuch', 0, '09/23/1987', 5, '+380993234'),
		   ('Sergiyi', 'Moroz', 'Bognanovuch', 0, '10/09/2002', 5, '+3809232243'),
		   ('Volodymyr', 'Nosorogov', 'Bognanovuch', 0, '07/09/2000', 1, '+3802344243'),
		   ('Vasyliy', 'Ivanov', 'Yuriyovuch', 0, '10/12/1998', 6, '+38099123343'),
		   ('Dmytriyi', 'Ivanov', 'Yuriyovuch', 0, '11/12/1998', 7, '+38099123343'),
		   ('Kateryna', 'Stepanchuk', 'Ivanivna', 1, '01/09/1997', 8, '+38066234934'),
		   ('Kateryna', 'Bondar', 'Mykhailovna', 1, '01/06/2000', 9, '+38066224233'),
		   ('Iryna', 'Fyinman', 'Ivanivna', 1, '01/08/1997', 9, '+3804334934'),
		   ('Olgha', 'Moroz', 'Sergiyivna', 1, '05/10/2001', 9, '+380423434234'),
		   ('Vasyliy', 'Moroz', 'Yuriyovuch', 0, '10/12/1998', 1, '+38099123343'),
		   ('Ivan', 'Bondarenko', 'Ivanovuch', 0, '10/11/1995', 2, '+38099123243'),
		   ('Sergiyi', 'Ivanov', 'Stepanovuch', 0, '10/12/1998', 9, '+380955094323'),
		   ('Sergiyi', 'Bondarenko', 'Yuriyovuch', 0, '10/09/1990', 9, '+3809934243'),
		   ('Sergiyi', 'Voron', 'Ivanovuch', 0, '09/23/1987', 9, '+380993234'),
		   ('Sergiyi', 'Moroz', 'Bognanovuch', 0, '10/09/2002', 9, '+3809232243'),
		   ('Volodymyr', 'Nosorogov', 'Bognanovuch', 0, '07/09/2000', 9, '+3802344243'),
		   ('Kateryna', 'Stepanchuk', 'Ivanivna', 1, '01/09/1997', 9, '+38066234934'),
		   ('Kateryna', 'Bondar', 'Mykhailovna', 1, '01/06/2000', 7, '+38066224233'),
		   ('Iryna', 'Fyinman', 'Ivanivna', 1, '01/08/1997', 1, '+3804334934'),
		   ('Olgha', 'Moroz', 'Sergiyivna', 1, '05/10/2001', 1, '+380423434234'),
		   ('Vasyliy', 'Moroz', 'Yuriyovuch', 0, '10/12/1998', 1, '+38099123343')
GO


INSERT INTO [dbo].[AcademicDegree]
           ([Name])
     VALUES
           ('None'),
		   ('PhD')
GO

INSERT INTO [dbo].[Degree]
           ([Name])
     VALUES
           ('Junior scientist'),
		   ('Senior scientist'),
		   ('Docent')
GO

INSERT INTO [dbo].[Position]
           ([Name])
     VALUES
           ('Aspirant'),
		   ('Professor'),
		   ('Docent')
GO


INSERT INTO [dbo].[Faculty]
           ([Name])
     VALUES
           ('Faculty 1'),
		   ('Faculty 2'),
		   ('Faculty 3')
GO

INSERT INTO [dbo].[ScientificPublisher]
           ([Name])
     VALUES
           ('SCOPUS Journal: Science'),
		   ('University Journal'),
		   ('Ukraine Journal')
GO

INSERT INTO dbo.StudyType
           ([Name])
     VALUES
           ('Offline'), ('Distance')
GO

INSERT INTO [dbo].[Student]
           ([PersonId]
           ,[IsContract]
           ,[StudyTypeId]
           ,[IsMilitaryObligatory]
		   ,[CourseNumber])
     VALUES
           (1,1,1,1,1),
		   (2,1,1,1,1),
		   (3,1,1,0,1),
		   (4,1,1,0,1),
		   (5,1,1,0,1),
		   (6,0,1,0,1),
		   (7,0,1,0,1),
		   (8,0,1,0,1),
		   (9,0,1,0,1),
		   (10,0,1,0,1),
		   (11,0,2,1,1),
		   (12,0,2,1,1),
		   (13,0,2,1,2),
		   (14,0,2,1,2),
		   (15,0,2,0,2),
		   (16,0,2,0,2),
		   (17,0,2,0,2),
		   (18,0,2,0,2),
		   (19,0,2,0,2),
		   (20,0,2,0,2),
		   (21,1,2,0,2),
		   (22,1,2,0,1),
		   (23,0,1,0,3),
		   (24,0,1,0,3),
		   (25,0,1,0,3)
GO


INSERT INTO [dbo].[SubjectMark]
           ([StudentId]
           ,[SubjectId]
           ,[Mark])
     VALUES
           (1, 1, 100),
		   (1, 2, 90),
		   (1, 3, 95),
		   (1, 4, 80),
		   (1, 5, 70),
		   (1, 6, 90),
		   (1, 7, 76),
		   (1, 8, 84),
		   (1, 9, 100),
		   (1, 10, 60),

		   (2, 11, 80),
		   (2, 12, 70),
		   (2, 13, 93),
		   (2, 14, 67),
		   (2, 15, 70),
		   (2, 16, 90),
		   (2, 17, 76),
		   (2, 18, 84),
		   (2, 19, 100),
		   (2, 20, 60),

		   (3, 11, 83),
		   (3, 10, 70),
		   (3, 13, 95),
		   (3, 14, 67),
		   (3, 15, 75),
		   (3, 16, 90),
		   (3, 17, 79),
		   (3, 18, 84),
		   (3, 19, 78),
		   (3, 1, 60),

		   (4, 1, 84),
		   (4, 2, 70),
		   (4, 3, 95),
		   (4, 5, 98),
		   (4, 15, 75),
		   (4, 6, 90),
		   (4, 7, 95),
		   (4, 8, 98),
		   (4, 9, 96),
		   (4, 10, 100),

		   (5, 1, 80),
		   (5, 2, 76),
		   (5, 3, 90),
		   (5, 5, 88),
		   (5, 15, 75),
		   (5, 6, 90),
		   (5, 7, 95),
		   (5, 8, 88),
		   (5, 9, 96),
		   (5, 10, 100),

		   (6, 6, 100),
		   (6, 2, 90),
		   (6, 3, 95),
		   (6, 4, 40),
		   (6, 5, 50),
		   (6, 6, 90),
		   (6, 7, 70),
		   (6, 8, 84),
		   (6, 9, 80),
		   (6, 11, 65),

		   (7, 11, 60),
		   (7, 17, 60),
		   (7, 13, 63),
		   (7, 14, 67),
		   (7, 15, 70),
		   (7, 16, 80),
		   (7, 17, 56),
		   (7, 18, 84),
		   (7, 19, 100),
		   (7, 20, 60),

		   (8, 11, 88),
		   (8, 10, 70),
		   (8, 9, 93),
		   (8, 14, 65),
		   (8, 15, 75),
		   (8, 16, 80),
		   (8, 17, 79),
		   (8, 18, 84),
		   (8, 19, 78),
		   (8, 1, 60),

		   (9, 1, 89),
		   (9, 2, 70),
		   (9, 3, 95),
		   (9, 5, 98),
		   (9, 15, 75),
		   (9, 6, 67),
		   (9, 7, 90),
		   (9, 8, 98),
		   (9, 9, 96),
		   (9, 10, 90),

		   (10, 1, 80),
		   (10, 2, 76),
		   (10, 3, 90),
		   (10, 10, 88),
		   (10, 11, 71),
		   (10, 6, 90),
		   (10, 7, 91),
		   (10, 8, 88),
		   (10, 9, 96),
		   (10, 10, 100),

		   (11, 11, 100),
		   (11, 2, 90),
		   (11, 3, 95),
		   (11, 4, 80),
		   (11, 5, 70),
		   (11, 6, 90),
		   (11, 7, 76),
		   (11, 8, 84),
		   (11, 9, 100),
		   (11, 12, 60),

		   (12, 11, 80),
		   (12, 12, 60),
		   (12, 13, 93),
		   (12, 14, 67),
		   (12, 15, 70),
		   (12, 16, 80),
		   (12, 17, 76),
		   (12, 18, 84),
		   (12, 19, 100),
		   (12, 20, 60),

		   (13, 11, 83),
		   (13, 10, 70),
		   (13, 13, 95),
		   (13, 14, 67),
		   (13, 15, 75),
		   (13, 16, 90),
		   (13, 17, 79),
		   (13, 18, 84),
		   (13, 19, 78),
		   (13, 1, 60),

		   (14, 1, 84),
		   (14, 2, 70),
		   (14, 3, 95),
		   (14, 5, 98),
		   (14, 15, 75),
		   (14, 6, 90),
		   (14, 7, 95),
		   (14, 8, 98),
		   (14, 9, 96),
		   (14, 10, 100),

		   (15, 1, 80),
		   (15, 2, 76),
		   (15, 3, 90),
		   (15, 5, 88),
		   (15, 15, 75),
		   (15, 6, 90),
		   (15, 7, 95),
		   (15, 8, 88),
		   (15, 9, 96),
		   (15, 10, 100),

		   (16, 6, 100),
		   (16, 2, 90),
		   (16, 3, 95),
		   (16, 4, 40),
		   (16, 5, 50),
		   (16, 6, 90),
		   (16, 7, 70),
		   (16, 8, 84),
		   (16, 9, 80),
		   (16, 11, 65),

		   (17, 11, 60),
		   (17, 17, 60),
		   (17, 13, 63),
		   (17, 14, 67),
		   (17, 15, 70),
		   (17, 16, 80),
		   (17, 17, 56),
		   (17, 18, 84),
		   (17, 19, 100),
		   (17, 20, 60),

		   (18, 11, 88),
		   (18, 10, 70),
		   (18, 9, 93),
		   (18, 14, 65),
		   (18, 15, 75),
		   (18, 16, 80),
		   (18, 17, 79),
		   (18, 18, 84),
		   (18, 19, 78),
		   (18, 1, 60),

		   (19, 1, 89),
		   (19, 2, 70),
		   (19, 3, 95),
		   (19, 5, 98),
		   (19, 15, 75),
		   (19, 6, 67),
		   (19, 7, 90),
		   (19, 8, 98),
		   (19, 9, 96),
		   (19, 10, 90),

		   (20, 1, 100),
		   (20, 2, 90),
		   (20, 3, 95),
		   (20, 4, 80),
		   (20, 5, 70),
		   (20, 6, 90),
		   (20, 7, 76),
		   (20, 8, 84),
		   (20, 9, 100),
		   (20, 10, 60),

		   (21, 1, 100),
		   (21, 2, 90),
		   (21, 3, 95),
		   (21, 4, 80),
		   (21, 5, 70),
		   (21, 6, 90),
		   (21, 7, 76),
		   (21, 8, 84),
		   (21, 9, 100),
		   (21, 10, 60),

		   (22, 11, 80),
		   (22, 12, 70),
		   (22, 13, 93),
		   (22, 14, 67),
		   (22, 15, 70),
		   (22, 16, 90),
		   (22, 17, 76),
		   (22, 18, 84),
		   (22, 19, 100),
		   (22, 20, 60),

		   (23, 11, 83),
		   (23, 10, 70),
		   (23, 13, 95),
		   (23, 14, 67),
		   (23, 15, 75),
		   (23, 16, 90),
		   (23, 17, 79),
		   (23, 18, 84),
		   (23, 19, 78),
		   (23, 1, 60),

		   (24, 1, 84),
		   (24, 2, 70),
		   (24, 3, 95),
		   (24, 5, 98),
		   (24, 15, 75),
		   (24, 6, 90),
		   (24, 7, 95),
		   (24, 8, 98),
		   (24, 9, 96),
		   (24, 10, 100),

		   (25, 1, 80),
		   (25, 2, 76),
		   (25, 3, 90),
		   (25, 5, 88),
		   (25, 15, 75),
		   (25, 6, 90),
		   (25, 7, 95),
		   (25, 8, 88),
		   (25, 9, 96),
		   (25, 10, 100)
GO

INSERT INTO [dbo].[StudentGroup]
           ([Name])
     VALUES
           ('KM'), ('KU'),('KC')
GO

INSERT INTO [dbo].[StudentGroupMember]
           ([GroupId]
           ,[StudentId])
     VALUES
           (3, 25), (3, 1), (3, 3), (3, 4), (3, 5), (3, 6), (3, 7), (3, 8), (3, 9), (3, 10), (3, 11), (3, 12),
		   (1, 6), (1, 7), (1, 8), (1, 9), (1, 10), (1, 11), (1, 12), (1, 13), (1, 14), (1, 15), (1, 16), (1, 17), (1, 18),
		   (2, 20), (2, 19), (2, 21), (2,22), (2, 23), (2,24), (2, 25)
GO


INSERT INTO [dbo].[Teacher]
           ([PersonId]
           ,[AcademicDegree]
           ,[Degree])
     VALUES
           (21, 1, 1), (22, 1, 2), (23, 1, 3), (24, 2, 1), (25, 2, 2)
GO

INSERT INTO [dbo].[TeacherPosition]
           ([TeacherId]
           ,[PositionId]
           ,[FacultyId])
     VALUES
           (1, 1, 1), (1, 2, 2), (2, 2, 3), (2, 1, 2), (3, 1, 1), (4, 2, 3), (5, 2, 2), (5, 1, 3)
GO

INSERT INTO [dbo].[ScientificType]
VALUES
	('Article'), ('Conference proceeding')
GO


INSERT INTO [dbo].[ScientificWork]
           ([Name]
           ,[ScientificTypeId]
           ,[PublisherId])
     VALUES
           ('Data analysis in Biology', 1, 1), ('Data analysis in Chemistry', 1, 1), ('Data analysis in Economy', 1, 2),
		   ('Comparison of cutting edge approaches', 1, 1), ('SQL Database approaches', 1, 1), ('Data mining in data science', 1,3),
		   ('English literature in 17th century', 1, 3), ('English literature in 19th century', 1, 1), ('English literature in 18th century', 1,3),
		   ('Neurosciene in 21th', 1,3),

		   ('Data analysis in Biology Conference', 2, 1), ('Data analysis in Chemistry Conference', 2, 1), ('Data analysis in Economy Conference', 2, 2),
		   ('Comparison of cutting edge approaches Conference', 2, 1), ('SQL Database approaches Conference', 2, 1), ('Data mining in data science Conference', 2,3),
		   ('English literature in 17th century Conference', 2, 3), ('English literature in 19th century Conference', 2, 1), ('English literature in 18th century Conference', 2,3),
		   ('Neurosciene in 21th Conference', 2,3)

GO




INSERT INTO [dbo].[ScientificWorkAuthor]
           ([ScientificWorkId]
           ,[PersonId])
     VALUES
           (1, 1), (1, 2), (1, 9), (1, 7), (1, 5), (19, 1), (19, 2), (19, 9), (19, 7), (19, 5),(20, 21), (20, 22), (20, 9), (20, 7), (20, 25),
		   (3, 9), (3, 2), (3, 7), (3, 7), (3, 5), (4, 9), (4, 2), (4, 7), (4, 7), (4, 5),(5, 9), (5, 2), (5, 7), (5, 7), (5, 25),
		   (9, 9), (9, 2), (9, 7), (9, 8), (9, 9), (7, 9), (7, 2), (7, 7), (7, 8), (7, 9),(8, 9), (8, 2), (8, 7), (8, 8), (8, 9),
		   (9, 9), (9, 2), (9, 7), (9, 8), (9, 9), (10, 9), (10, 2), (10, 7), (10, 8), (10, 9),(11, 9), (11, 22), (11, 7), (11, 8), (11, 9),
		   (12, 1), (12, 2), (12, 9), (12, 7), (12, 5), (19, 21), (19, 22), (19, 9), (19, 7), (19, 25),(20, 1), (20, 2), (20, 9), (20, 7), (20, 5),
		   (19, 9), (19, 7), (15, 8), (15, 9), (15, 25), (19, 1), (19, 2), (19, 9), (19, 7), (19, 5),(20, 1), (20, 2), (20, 9), (20, 7), (20, 5),
		   (18, 1), (18, 2), (18, 9), (18, 20), (18, 5), (19, 1), (19, 22), (19, 9), (19, 27), (19, 25),(20, 21), (20, 2), (20, 9), (20, 7), (20, 25)
GO


