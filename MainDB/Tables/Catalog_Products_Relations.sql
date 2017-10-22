CREATE TABLE [dbo].[Catalog_Products_Relations]
(
	[Id] INT NOT NULL IDENTITY,
	[GroupId] UNIQUEIDENTIFIER NOT NULL , 
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_Catalog_Products_Relations_CatalogProducts] FOREIGN KEY ([ProductId]) REFERENCES [Catalog_Products]([Id]), 
    CONSTRAINT [PK_Catalog_Products_Relations] PRIMARY KEY ([Id])
)
