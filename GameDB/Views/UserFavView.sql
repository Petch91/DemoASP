CREATE VIEW [dbo].[UserFavView]
	AS
	SELECT g.Id, g.Title, g.DateSortie, g.[Resume],g.GenreId,g.[Label], ufg.UserId FROM GameGenreView g 
	join UserFavGames ufg
	ON g.Id = ufg.GameId 