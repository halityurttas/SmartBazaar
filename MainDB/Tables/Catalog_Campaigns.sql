CREATE TABLE [dbo].[Catalog_Campaigns]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CampaignMethod] SMALLINT NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [DiscountMethod] SMALLINT NOT NULL, 
    [DiscountValue] FLOAT NOT NULL, 
	[Title] NVARCHAR(250) NOT NULL, 
    [Description] NTEXT NULL, 
    [SliderUrl] VARCHAR(250) NULL,
	[Status] SMALLINT NOT NULL DEFAULT 1, 
    [MaxUsage] INT NOT NULL DEFAULT 0
    
)
