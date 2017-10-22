ALTER TABLE [dbo].[Payment_Installment]
	ADD CONSTRAINT [PaymentInstallmentConstraint]
	FOREIGN KEY (PaymentId)
	REFERENCES [Payment_Types] (Id)
