CREATE PROCEDURE [dbo].[UserLogin]
	@email VARCHAR(100),
	@pwd VARCHAR(100)

AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SET @salt = (SELECT Salt FROM Users WHERE Email = @email)
	
	DECLARE @pwdHash VARBINARY(64)
	SET @pwdHash = HASHBYTES('SHA2_512',CONCAT(@pwd,@salt,dbo.GetSecretKey()))

	SELECT Id, Email, Username, RoleId FROM Users WHERE Email = @email AND PasswordHash = @pwdHash

END