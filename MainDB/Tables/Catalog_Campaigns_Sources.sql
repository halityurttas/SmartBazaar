CREATE TABLE [dbo].[Catalog_Campaigns_Sources]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CampaignId] INT NOT NULL, 
    [ProductId] INT NOT NULL
)
