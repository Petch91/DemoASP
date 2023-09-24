CREATE TABLE [dbo].[User]
(
	[Id] uniqueidentifier NOT NULL PRIMARY KEY default Newid(), 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Role] INT NOT NULL DEFAULT 0, 
    [Email] VARCHAR(50) NOT NULL
	
)
