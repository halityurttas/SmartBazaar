ALTER TABLE [dbo].[Payment_Entities]
	ADD CONSTRAINT [PaymentEntityTypeConstraint]
	FOREIGN KEY (PaymentTypeId)
	REFERENCES [Payment_Types] (Id)
