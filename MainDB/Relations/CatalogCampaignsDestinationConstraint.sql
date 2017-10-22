ALTER TABLE [dbo].[Catalog_Campaigns_Destinations]
	ADD CONSTRAINT [CatalogCampaignsDestinationConstraint]
	FOREIGN KEY (CampaignId)
	REFERENCES [Catalog_Campaigns] (Id)
