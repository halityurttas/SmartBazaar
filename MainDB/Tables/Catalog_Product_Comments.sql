CREATE TABLE [dbo].[Catalog_Product_Comments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [UserTitle] NVARCHAR(150) NOT NULL, 
    [Rating] SMALLINT NOT NULL, 
    [Description] NTEXT NOT NULL, 
    [Status] SMALLINT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Created] DATETIME NOT NULL, 
    CONSTRAINT [FK_Catalog_Product_Comments_Product] FOREIGN KEY ([ProductId]) REFERENCES [Catalog_Products]([Id]) 
)
