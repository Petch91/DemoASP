CREATE PROCEDURE [dbo].[UserRegister]
	@email VARCHAR(100),
	@password VARCHAR(100),
	@username VARCHAR(100)

AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SET @salt = CONCAT(NEWID(),NEWID(),NEWID())
	
	DECLARE @pwdHash VARBINARY(64)
	SET @pwdHash = HASHBYTES('SHA2_512',CONCAT(@password,@salt,dbo.GetSecretKey()))

	INSERT INTO [USERS] (Email, Username, PasswordHash, Salt)
	VALUES (@email, @username, @pwdHash, @salt)

END
