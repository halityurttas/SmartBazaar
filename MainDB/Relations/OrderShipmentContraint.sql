ALTER TABLE [dbo].[Order_Heads]
	ADD CONSTRAINT [OrderShipmentConstraint]
	FOREIGN KEY (ShipmentTypeId)
	REFERENCES [Shipment_Types] (Id)
