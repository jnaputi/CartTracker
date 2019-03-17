﻿USE [CartTracker]

CREATE TABLE [Carts] (
	[CartId] INT PRIMARY KEY IDENTITY(1, 1),
	[Name] NVARCHAR(128) UNIQUE NOT NULL,
	[Description] NVARCHAR(256),
	[DateCreated] DATETIME NOT NULL,
	[LastUpdated] DATETIME NOT NULL,
)
