ALTER TABLE [dbo].[Catalog_Products_Properties]
	ADD CONSTRAINT [ProductPropertiesConstraint]
	FOREIGN KEY (ProductId)
	REFERENCES [Catalog_Products] (Id)
