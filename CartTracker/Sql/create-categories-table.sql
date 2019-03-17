USE [CartTracker]

CREATE TABLE [Categories] (
	[CategoryId] INT PRIMARY KEY IDENTITY(1, 1),
	[ItemId] INT FOREIGN KEY REFERENCES [dbo].[Items]([ItemId]),
	[Name] NVARCHAR(64) NOT NULL UNIQUE,
	[DateCreated] DATETIME NOT NULL,
	[LastUpdated] DATETIME NOT NULL
)
