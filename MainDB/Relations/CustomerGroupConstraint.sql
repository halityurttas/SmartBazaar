ALTER TABLE [dbo].[Customer_Entities]
	ADD CONSTRAINT [CustomerGroupConstraint]
	FOREIGN KEY (GroupId)
	REFERENCES [Customer_Groups] (Id)
