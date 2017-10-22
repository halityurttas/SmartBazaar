ALTER TABLE [dbo].[Order_Lines]
	ADD CONSTRAINT [CatalogCampaignsOrderLinesConstraint]
	FOREIGN KEY (CampaignId)
	REFERENCES [Catalog_Campaigns] (Id)
