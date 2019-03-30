USE CartTracker

CREATE TABLE ItemCategories (
    ItemCategoryId INT PRIMARY KEY IDENTITY(1, 1),
    ItemId INT NOT NULL FOREIGN KEY REFERENCES Items(ItemId),
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId),
    LastUpdated DATETIME NOT NULL
)
