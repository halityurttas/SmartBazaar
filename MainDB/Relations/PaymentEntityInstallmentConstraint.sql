ALTER TABLE [dbo].[Payment_Entities]
	ADD CONSTRAINT [PaymentEntityInstallmentConstraint]
	FOREIGN KEY (InstallmentId)
	REFERENCES [Payment_Installment] (Id)
