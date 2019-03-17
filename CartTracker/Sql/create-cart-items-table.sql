USE [CartTracker]

CREATE TABLE [CartItems] (
	[CartItemId] INT PRIMARY KEY IDENTITY(1, 1),
	[CartId] INT NOT NULL FOREIGN KEY REFERENCES [Carts]([CartId]),
	[ItemId] INT FOREIGN KEY REFERENCES [Items]([ItemId]),
	[LastUpdated] DATETIME NOT NULL
)
