CREATE TABLE [dbo].[Catalog_Brands]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Status] SMALLINT NOT NULL DEFAULT 1, 
    [ExternalRef1] VARCHAR(100) NULL, 
    [ExternalRef2] VARCHAR(100) NULL, 
    [ExternalRef3] VARCHAR(100) NULL
)
