CREATE TABLE [dbo].[Order_Lines]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Price] MONEY NOT NULL, 
    [TaxRate] FLOAT NOT NULL, 
    [Total] MONEY NOT NULL, 
    [CampaignId] INT NULL
)
