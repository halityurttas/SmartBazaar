CREATE TABLE [dbo].[Payment_Types]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Method] SMALLINT NOT NULL, 
    [CommissionRate] FLOAT NOT NULL DEFAULT 0, 
    [ProcessFee] MONEY NOT NULL DEFAULT 0, 
	[Title] NVARCHAR(100) NOT NULL,
    [Status] SMALLINT NOT NULL DEFAULT 1, 
    [Description] NTEXT NULL, 
    [PosType] VARCHAR(50) NULL
)
