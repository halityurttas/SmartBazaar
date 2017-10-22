CREATE TABLE [dbo].[Pages_Lang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RefId] INT NOT NULL, 
    [Code] VARCHAR(10) NOT NULL,
	[Title] NVARCHAR(250) NOT NULL, 
    [Detail] NTEXT NULL, 
    CONSTRAINT [FK_Pages_Lang_ToParent] FOREIGN KEY ([RefId]) REFERENCES [Pages]([Id])
)
