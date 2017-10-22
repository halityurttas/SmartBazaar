CREATE TABLE [dbo].[Payment_Types_Lang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RefId] INT NOT NULL, 
    [Code] VARCHAR(10) NOT NULL, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Description] NTEXT NULL, 
    CONSTRAINT [FK_Payment_Types_Lang_ToParent] FOREIGN KEY ([RefId]) REFERENCES [Payment_Types]([Id])
)
