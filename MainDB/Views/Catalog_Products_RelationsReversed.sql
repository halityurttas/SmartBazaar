CREATE VIEW [dbo].[Catalog_Products_RelationsReversed]
	AS 
	SELECT RTRight.Id, RTLeft.Id AS Catalog_Products_Relations_Id, RTRight.ProductId AS RightProductId FROM [Catalog_Products_Relations] RTLeft
	INNER JOIN [Catalog_Products_Relations] RTRight ON RTLeft.GroupId = RTRight.GroupId
