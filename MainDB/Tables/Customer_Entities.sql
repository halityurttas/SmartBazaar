CREATE TABLE [dbo].[Customer_Entities]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [GroupId] INT NULL, 
    [Title] NVARCHAR(150) NOT NULL, 
    [TaxNr] VARCHAR(25) NULL, 
    [TaxOffice] NVARCHAR(100) NULL, 
    [CustomerType] SMALLINT NOT NULL, 
    [BirthDate] DATETIME NULL, 
    [Gender] SMALLINT NULL, 
    [Company] NVARCHAR(250) NULL, 
    [ContactPhone] VARCHAR(25) NULL, 
    [Status] SMALLINT NOT NULL DEFAULT 1, 
    [Created] DATETIME NOT NULL DEFAULT GETDATE() 
)
