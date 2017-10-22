ALTER TABLE [dbo].[Catalog_Campaigns_Sources]
	ADD CONSTRAINT [CatalogCampaignsSourceProductConstraint]
	FOREIGN KEY (ProductId)
	REFERENCES [Catalog_Products] (Id)
