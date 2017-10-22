CREATE TABLE [dbo].[Catalog_Product_Images]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductId] INT NOT NULL, 
    [ImageUrl] VARCHAR(250) NOT NULL, 
    [Sort] INT NOT NULL DEFAULT 1
)
