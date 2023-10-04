CREATE TABLE [dbo].[GameGenre]
(
	[GameId] INT NOT NULL, 
   [GenreId] INT NOT NULL,
	PRIMARY KEY (GameId,GenreId),
	CONSTRAINT [FK_GameGenre_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id]), 
   CONSTRAINT [FK_JeuCat_Categorie] FOREIGN KEY ([GenreId]) REFERENCES [Genre]([GenreId])
)
