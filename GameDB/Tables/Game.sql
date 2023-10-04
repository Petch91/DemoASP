CREATE TABLE [dbo].[Game]
(
	[Id] INT NOT NULL identity PRIMARY KEY, 
    [Title] VARCHAR(50) NOT NULL, 
    [Resume] VARCHAR(200) NULL, 
    [DateSortie] DATETIME2 NOT NULL
)
