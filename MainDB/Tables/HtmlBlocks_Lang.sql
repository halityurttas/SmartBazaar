CREATE TABLE [dbo].[HtmlBlocks_Lang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RefId] INT NOT NULL, 
    [Code] VARCHAR(10) NOT NULL, 
    [Title] NVARCHAR(150) NOT NULL, 
    [Detail] NTEXT NULL, 
    CONSTRAINT [FK_HtmlBlocks_Lang_ToParent] FOREIGN KEY ([RefId]) REFERENCES [HtmlBlocks]([Id]), 
)
