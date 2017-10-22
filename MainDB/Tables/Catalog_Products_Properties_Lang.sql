CREATE TABLE [dbo].[Catalog_Products_Properties_Lang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RefId] INT NOT NULL, 
	[Code] VARCHAR(10) NOT NULL,
    [Title] NVARCHAR(150) NOT NULL, 
    [Value] NVARCHAR(250) NOT NULL, 
    CONSTRAINT [FK_Catalog_Products_Properties_Lang_ToParent] FOREIGN KEY ([RefId]) REFERENCES [Catalog_Products_Properties]([Id])
)
