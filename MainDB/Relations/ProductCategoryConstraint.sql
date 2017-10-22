ALTER TABLE [dbo].[Catalog_Products]
	ADD CONSTRAINT [ProductCategoryConstraint]
	FOREIGN KEY (CategoryId)
	REFERENCES [Catalog_Categories] (Id)
