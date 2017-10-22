ALTER TABLE [dbo].[Catalog_Campaigns_Destinations]
	ADD CONSTRAINT [CatalogCampaignsDestinationsProductConstraint]
	FOREIGN KEY (ProductId)
	REFERENCES [Catalog_Products] (Id)
