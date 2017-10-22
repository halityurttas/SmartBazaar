ALTER TABLE [dbo].[Catalog_Campaigns_Sources]
	ADD CONSTRAINT [CatalogCampaignsSourcesConstraint]
	FOREIGN KEY (CampaignId)
	REFERENCES [Catalog_Campaigns] (Id)
