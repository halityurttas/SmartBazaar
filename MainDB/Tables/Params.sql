CREATE TABLE [dbo].[Params]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(250) NOT NULL, 
    [GroupId] INT NOT NULL, 
    [Value] NVARCHAR(250) NOT NULL, 
    CONSTRAINT [FK_Params_ToParamsGroups] FOREIGN KEY ([GroupId]) REFERENCES [Params_Groups]([Id])
)
