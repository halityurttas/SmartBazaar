CREATE TABLE [dbo].[Catalog_Categories_Lang]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [RefId] INT NOT NULL, 
    [Code] VARCHAR(10) NOT NULL, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Description] NTEXT NULL, 
    CONSTRAINT [FK_Catalog_Categories_Lang_ToParent] FOREIGN KEY ([RefId]) REFERENCES [Catalog_Categories]([Id])
)
