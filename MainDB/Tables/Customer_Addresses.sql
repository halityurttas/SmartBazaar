CREATE TABLE [dbo].[Customer_Addresses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [Title] NVARCHAR(150) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [District] NVARCHAR(50) NULL, 
    [Town] NVARCHAR(50) NULL, 
    [Detail] NTEXT NULL, 
    [Status] SMALLINT NOT NULL DEFAULT 1, 
    [IsDefault] BIT NOT NULL DEFAULT 0, 
    [PostalCode] VARCHAR(15) NULL 
)
