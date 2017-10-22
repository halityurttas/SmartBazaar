CREATE TABLE [dbo].[Catalog_Categories]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParentId] INT NULL, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Description] NTEXT NULL, 
    [Pos] INT NOT NULL DEFAULT 1, 
    [Level] INT NOT NULL DEFAULT 0, 
    [Status] SMALLINT NOT NULL DEFAULT 1, 
    [ExternalRef1] VARCHAR(100) NULL, 
    [ExternalRef2] VARCHAR(100) NULL, 
    [ExternalRef3] VARCHAR(100) NULL, 
    [IsDisplayInMenu] BIT NOT NULL DEFAULT 0, 
    [ImageUrl] NVARCHAR(250) NULL, 
    [IsDisplayInMainPage] BIT NOT NULL DEFAULT 0
)
