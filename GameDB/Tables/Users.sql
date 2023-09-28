CREATE TABLE [dbo].[Users]
(
	 [Id] uniqueidentifier NOT NULL PRIMARY KEY default Newid(),
    [Email] VARCHAR(100) NOT NULL UNIQUE,
    [Username] VARCHAR(100) NOT NULL UNIQUE, 
    [PasswordHash] VARBINARY(64) NOT NULL, 
    [Salt] VARCHAR(100),
    RoleId int DEFAULT 1 NOT NULL
)
