CREATE VIEW [dbo].[GameGenreView]

	AS select j.Id, j.Title, j.DateSortie, j.[Resume],c.GenreId, c.[Label]
		FROM Game j join GameGenre jc
		ON j.Id = jc.GameId
		join Genre c
		ON c.GenreId = jc.GenreId