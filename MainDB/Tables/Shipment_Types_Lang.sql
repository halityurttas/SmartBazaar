CREATE TABLE [dbo].[Shipment_Types_Lang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RefId] INT NOT NULL, 
    [Code] VARCHAR(10) NOT NULL, 
    [Title] NVARCHAR(250) NOT NULL, 
    CONSTRAINT [FK_Shipment_Types_Lang_ToParent] FOREIGN KEY ([RefId]) REFERENCES [Shipment_Types]([Id])
)
