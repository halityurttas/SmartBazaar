ALTER TABLE [dbo].[Catalog_Product_Images]
	ADD CONSTRAINT [ProductImagesConstraints]
	FOREIGN KEY (ProductId)
	REFERENCES [Catalog_Products] (Id)
