ALTER TABLE [dbo].[Order_Heads]
	ADD CONSTRAINT [OrderInvoiceAddressConstraint]
	FOREIGN KEY (InvoiceAddressId)
	REFERENCES [Customer_Addresses] (Id)