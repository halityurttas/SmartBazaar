CREATE TABLE [dbo].[Catalog_Products_Lang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RefId] INT NOT NULL, 
    [Code] VARCHAR(10) NOT NULL,
	[Title] NVARCHAR(200) NOT NULL, 
    [Description] NTEXT NULL,
	[MetaTitle] NVARCHAR(100) NULL, 
    [MetaKeywords] NVARCHAR(150) NULL, 
    [MetaDescription] NVARCHAR(250) NULL,
	[ShortDescription] NTEXT NULL,
	[MetaUrl] VARCHAR(250) NULL, 
    CONSTRAINT [FK_Catalog_Products_Lang_ToParent] FOREIGN KEY ([RefId]) REFERENCES [Catalog_Products]([Id])
)
