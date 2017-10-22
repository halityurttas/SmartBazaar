﻿CREATE TABLE [dbo].[Tax_Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(150) NOT NULL, 
    [Rate] FLOAT NOT NULL DEFAULT 0, 
    [Status] SMALLINT NOT NULL DEFAULT 1 
)
