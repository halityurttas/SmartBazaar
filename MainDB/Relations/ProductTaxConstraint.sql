ALTER TABLE [dbo].[Catalog_Products]
	ADD CONSTRAINT [ProductTaxConstraint]
	FOREIGN KEY (TaxGroup)
	REFERENCES [Tax_Products] (Id)
