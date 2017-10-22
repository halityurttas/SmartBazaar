CREATE TABLE [dbo].[Catalog_Brands_Lang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RefId] INT NOT NULL, 
    [Code] VARCHAR(10) NOT NULL, 
    [Title] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_Catalog_Brands_Lang_ToParent] FOREIGN KEY ([RefId]) REFERENCES [Catalog_Brands]([Id])
)
