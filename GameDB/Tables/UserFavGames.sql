CREATE TABLE [dbo].[UserFavGames]
(
	[UserId] uniqueidentifier NOT NULL, 
   [GameId] INT NOT NULL,
	PRIMARY KEY (UserId,GameId),
	CONSTRAINT [FK_UserFavGames_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), 
   CONSTRAINT [FK_UserFavGames_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id])
)
