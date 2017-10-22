CREATE TABLE [dbo].[Order_Heads]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [ShipAddressId] INT NULL, 
    [InvoiceAddressId] INT NULL, 
    [OrderDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [OrderTotal] MONEY NOT NULL, 
    [TaxTotal] MONEY NOT NULL, 
	[ShipmentTypeId] INT NOT NULL,
	[ShipCost] MONEY NOT NULL DEFAULT 0,
    [GrandTotal] MONEY NOT NULL, 
    [Status] SMALLINT NOT NULL DEFAULT 1, 
    [PaymentFee] MONEY NOT NULL, 
    [InstallmentFee] MONEY NOT NULL, 
    [Note] NTEXT NULL
    
)
