CREATE TABLE [dbo].[Pos_Settings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Data] NTEXT NOT NULL, 
    [AssemblyData] NVARCHAR(100) NOT NULL, 
    [Referance] NVARCHAR(100) NOT NULL, 
    [Title] NVARCHAR(250) NOT NULL, 
    [Status] SMALLINT NOT NULL
)
