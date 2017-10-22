CREATE TABLE [dbo].[Payment_Installment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PaymentId] INT NOT NULL, 
    [NumberOf] INT NOT NULL, 
    [Rate] FLOAT NOT NULL DEFAULT 0
)
