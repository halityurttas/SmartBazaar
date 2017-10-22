CREATE TABLE [dbo].[Lang_Dictionary]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BookId] INT NOT NULL, 
    [Key] NVARCHAR(250) NOT NULL, 
    [Value] NTEXT NOT NULL, 
    CONSTRAINT [FK_Lang_Dictionary_ToBooks] FOREIGN KEY ([BookId]) REFERENCES [Lang_Books]([Id]) 
)
