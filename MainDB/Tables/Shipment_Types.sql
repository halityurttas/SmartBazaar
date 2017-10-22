CREATE TABLE [dbo].[Shipment_Types]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PricingMethod] SMALLINT NOT NULL, 
    [PricingValue] FLOAT NOT NULL, 
	[Title] NVARCHAR(250) NOT NULL,
    [Status] SMALLINT NOT NULL DEFAULT 1
)
