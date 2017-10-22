﻿CREATE TABLE [dbo].[Pages]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Status] SMALLINT NOT NULL DEFAULT 1, 
    [Title] NVARCHAR(250) NOT NULL, 
    [Detail] NTEXT NULL, 
    [IsFixed] BIT NOT NULL DEFAULT 0
)
