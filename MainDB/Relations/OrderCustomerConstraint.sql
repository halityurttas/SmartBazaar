ALTER TABLE [dbo].[Order_Heads]
	ADD CONSTRAINT [OrderCustomerConstraint]
	FOREIGN KEY (CustomerId)
	REFERENCES [Customer_Entities] (Id)
