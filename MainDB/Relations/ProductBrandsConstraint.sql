ALTER TABLE [dbo].[Catalog_Products]
	ADD CONSTRAINT [ProductBrandsConstraint]
	FOREIGN KEY (BrandId)
	REFERENCES [Catalog_Brands] (Id)
