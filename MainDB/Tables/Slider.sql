﻿CREATE TABLE [dbo].[Slider]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(250) NOT NULL, 
    [Detail] NTEXT NULL, 
    [ImageUrl] NVARCHAR(100) NOT NULL, 
    [Status] SMALLINT NOT NULL
)