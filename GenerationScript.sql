USE [master]
GO
/****** Object:  Database [TestBD]    Script Date: 29.07.2023 13:16:50 ******/
CREATE DATABASE [TestBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TestBD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TestBD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TestBD] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestBD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestBD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TestBD] SET  MULTI_USER 
GO
ALTER DATABASE [TestBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestBD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestBD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TestBD] SET QUERY_STORE = OFF
GO
USE [TestBD]
GO
/****** Object:  UserDefinedFunction [dbo].[GetRatingUser]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetRatingUser](@userId NVARCHAR(255), @ratingMultiplier INT) RETURNS INTEGER
AS
BEGIN   
  RETURN @ratingMultiplier * (SELECT COUNT(*) FROM Answers WHERE Answers.UserId = @userId)
END

GO
/****** Object:  Table [dbo].[Answers]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[Id] [nvarchar](36) NOT NULL,
	[QuestionId] [nvarchar](36) NOT NULL,
	[UserId] [nvarchar](36) NOT NULL,
	[Text] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [nvarchar](36) NOT NULL,
	[AnswerId] [nvarchar](36) NOT NULL,
	[UserId] [nvarchar](36) NOT NULL,
	[Text] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DizLikes]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DizLikes](
	[Id] [nvarchar](36) NOT NULL,
	[MessageId] [nvarchar](36) NOT NULL,
	[UserId] [nvarchar](36) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Likes]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Likes](
	[Id] [nvarchar](36) NOT NULL,
	[MessageId] [nvarchar](36) NOT NULL,
	[UserId] [nvarchar](36) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[Id] [nvarchar](36) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[UserId] [nvarchar](36) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Views] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](36) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [uq] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [Views]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([AnswerId])
REFERENCES [dbo].[Answers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[DizLikes]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[GetAnswersToQuestion]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAnswersToQuestion]
	@userId NVARCHAR(255),
	@idQuestion NVARCHAR(255),
	@count INT,
	@startCount INT,
	@ratingMultiplier INT
AS
SELECT *, (SELECT [Name] FROM Users WHERE Answers.UserId = Users.Id) AS [UserName],
	--(SELECT COUNT(*) * @ratingMultiplier FROM Comments WHERE Comments.AnswerId = Answers.Id) AS Rating,
	 (SELECT dbo.GetRatingUser(Answers.UserId, @ratingMultiplier)) AS Rating,
	--@ratingMultiplier * (SELECT COUNT(*) FROM Answers WHERE UserId = Answers.Id) AS Rating,
	--@ratingMultiplier * (SELECT COUNT(*) FROM Comments WHERE Comments.AnswerId = Answers.Id) AS Rating,
	(SELECT COUNT(*) FROM Comments WHERE Answers.Id = Comments.AnswerId) AS CountComments,
	CASE
        WHEN EXISTS (SELECT * FROM Likes WHERE Likes.UserId = @userId AND Likes.MessageId = Answers.Id)
               THEN 'true'
               ELSE 'false'
    END as 'Like',
	CASE
        WHEN EXISTS (SELECT * FROM DizLikes WHERE DizLikes.UserId = @userId AND DizLikes.MessageId = Answers.Id)
               THEN 'true'
               ELSE 'false'
    END as 'DizLike',
	(SELECT COUNT(*) FROM Likes WHERE Likes.MessageId = Answers.Id) AS CountLike,
	(SELECT COUNT(*) FROM DizLikes WHERE DizLikes.MessageId = Answers.Id) AS CountDizLike
    FROM Answers WHERE Answers.QuestionId = @idQuestion 
    ORDER BY Rating OFFSET @startCount ROWS FETCH NEXT @count ROWS ONLY
GO
/****** Object:  StoredProcedure [dbo].[GetCommentsOfAnswer]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCommentsOfAnswer]
	@userId NVARCHAR(255),
	@idAnswer NVARCHAR(255),
	@count INT,
	@startCount INT,
	@ratingMultiplier INT
AS
SELECT *, 
	(SELECT dbo.GetRatingUser(Comments.UserId, @ratingMultiplier)) AS Rating,
	(SELECT [Name] FROM Users WHERE Comments.UserId = Users.Id) AS UserName,
	CASE
        WHEN EXISTS (SELECT * FROM Likes WHERE Likes.UserId = @userId AND Likes.MessageId = Comments.Id)
               THEN 'true'
               ELSE 'false'
    END as 'Like',
	CASE
        WHEN EXISTS (SELECT * FROM DizLikes WHERE DizLikes.UserId = @userId AND DizLikes.MessageId = Comments.Id)
               THEN 'true'
               ELSE 'false'
    END as 'DizLike',
	(SELECT COUNT(*) FROM Likes WHERE Likes.MessageId = Comments.Id) AS CountLike,
	(SELECT COUNT(*) FROM DizLikes WHERE DizLikes.MessageId = Comments.Id) AS CountDizLike
	FROM Comments WHERE Comments.AnswerId = @idAnswer 
	ORDER BY Rating OFFSET @startCount ROWS FETCH NEXT @count ROWS ONLY
GO
/****** Object:  StoredProcedure [dbo].[GetQuestionsFromUser]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetQuestionsFromUser]
	@userId NVARCHAR(255),
	@count INT,
	@startCount INT,
	@ratingMultiplier INT
AS
SELECT *, (SELECT [Name] FROM Users WHERE Id = Questions.UserId) AS UserName,
	(SELECT dbo.GetRatingUser(Questions.UserId, @ratingMultiplier)) AS Rating,	
	CASE
        WHEN EXISTS (SELECT * FROM Likes WHERE Likes.UserId = @userId AND Likes.MessageId = Questions.Id)
               THEN 'true'
               ELSE 'false'
    END as 'Like',
	CASE
        WHEN EXISTS (SELECT * FROM DizLikes WHERE DizLikes.UserId = @userId AND DizLikes.MessageId = Questions.Id)
               THEN 'true'
               ELSE 'false'
    END as 'DizLike',
	(SELECT COUNT(*) FROM Likes WHERE Likes.MessageId = Questions.Id) AS CountLike,
	(SELECT COUNT(*) FROM DizLikes WHERE DizLikes.MessageId = Questions.Id) AS CountDizLike,
	(SELECT COUNT(*) FROM Answers WHERE Questions.Id = Answers.QuestionId) AS CountAnswers
	FROM Questions
	WHERE Questions.UserId = @userId 
	ORDER BY Rating OFFSET @startCount ROWS FETCH NEXT @count ROWS ONLY
GO
/****** Object:  StoredProcedure [dbo].[GetRandomQuestions]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRandomQuestions]
	@userId NVARCHAR(255),
	@count INT,
	@startCount INT,
	@ratingMultiplier INT
AS
SELECT *, (SELECT [Name] FROM Users WHERE Id = Questions.UserId) AS UserName,	
(SELECT dbo.GetRatingUser(Questions.UserId, @ratingMultiplier)) AS Rating,
	CASE
        WHEN EXISTS (SELECT * FROM Likes WHERE Likes.UserId = @userId AND Likes.MessageId = Questions.Id)
               THEN 'true'
               ELSE 'false'
    END as 'Like',
	CASE
        WHEN EXISTS (SELECT * FROM DizLikes WHERE DizLikes.UserId = @userId AND DizLikes.MessageId = Questions.Id)
               THEN 'true'
               ELSE 'false'
    END as 'DizLike',
	(SELECT COUNT(*) FROM Likes WHERE Likes.MessageId = Questions.Id) AS CountLike,
	(SELECT COUNT(*) FROM DizLikes WHERE DizLikes.MessageId = Questions.Id) AS CountDizLike,
	(SELECT COUNT(*) FROM Answers WHERE Questions.Id = Answers.QuestionId) AS CountAnswers
	FROM Questions ORDER BY Rating OFFSET @startCount ROWS FETCH NEXT @count ROWS ONLY
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 29.07.2023 13:16:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUser]
	@userId NVARCHAR(255),
	@ratingMultiplier INT
AS
SELECT *,
(SELECT dbo.GetRatingUser(@userId, @ratingMultiplier)) AS Rating,
--(SELECT COUNT(*) * @ratingMultiplier FROM Answers WHERE Answers.UserId = @userId) AS Rating,
(SELECT COUNT(*) FROM Questions WHERE (SELECT COUNT(*) FROM Answers WHERE Answers.UserId = @userId AND Answers.QuestionId = Questions.Id) >= 1) AS [CountAnswers], 
(SELECT COUNT(*) FROM Questions WHERE Questions.UserId = @userId) AS [CountMyQuestions] 
FROM Users WHERE Users.Id = @userId
GO
USE [master]
GO
ALTER DATABASE [TestBD] SET  READ_WRITE 
GO
