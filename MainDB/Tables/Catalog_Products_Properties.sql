CREATE TABLE [dbo].[Catalog_Products_Properties]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductId] INT NOT NULL, 
    [Title] NVARCHAR(150) NOT NULL, 
    [Value] NVARCHAR(250) NOT NULL
)
