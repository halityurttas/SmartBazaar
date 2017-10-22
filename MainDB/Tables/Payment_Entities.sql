CREATE TABLE [dbo].[Payment_Entities]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderId] INT NOT NULL, 
    [PaymentTypeId] INT NOT NULL, 
    [InstallmentId] INT NULL, 
    [OrderPrice] MONEY NOT NULL, 
    [PaymentFee] MONEY NOT NULL, 
    [InstallmentFee] MONEY NOT NULL, 
    [ShipmentFee] MONEY NOT NULL, 
    [Total] MONEY NOT NULL, 
    [PaymentDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [Log] NTEXT NOT NULL, 
    [Status] SMALLINT NOT NULL DEFAULT 0
)
