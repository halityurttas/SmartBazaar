ALTER TABLE [dbo].[Customer_Addresses]
	ADD CONSTRAINT [CustomerAddressConstraint]
	FOREIGN KEY (CustomerId)
	REFERENCES [Customer_Entities] (Id)
