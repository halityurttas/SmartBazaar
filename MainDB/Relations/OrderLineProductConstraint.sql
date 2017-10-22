ALTER TABLE [dbo].[Order_Lines]
	ADD CONSTRAINT [OrderLineProductConstraint]
	FOREIGN KEY (ProductId)
	REFERENCES [Catalog_Products] (Id)
