USE [CartTracker]

CREATE TABLE [Categories] (
	[CategoryId] INT PRIMARY KEY IDENTITY(1, 1),
	[Name] NVARCHAR(64) NOT NULL UNIQUE,
	[DateCreated] DATETIME NOT NULL,
	[LastUpdated] DATETIME NOT NULL
)
