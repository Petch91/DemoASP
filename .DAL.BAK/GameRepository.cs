
using DAL.Interfaces;
using DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class GameRepository : BaseRepository<int, Game>, IGameRepository
   {
      public GameRepository(SqlConnection cnx) : base(cnx, "GameGenreView", "Id")
      {
      }

      public override Game Mapper(SqlDataReader record)
      {
         return new Game
         {
            Id = (int)record["Id"],
            Title = (string)record["Title"],
            Resume = record["Resume"] == DBNull.Value ? null : (string)record["Resume"],
            Genres = new Genre { Id = (int)record["GenreId"], Label = (string)record["Label"] },
            DateDeSortie = (DateTime)record["DateSortie"]
         };
      }
      public Game MapperCreate(SqlDataReader record)
      {
         return new Game
         {
            Id = (int)record["Id"],
            Title = (string)record["Title"],
            Resume = record["Resume"] == DBNull.Value ? null : (string)record["Resume"],
            DateDeSortie = (DateTime)record["DateSortie"]
         };
      }

      public override Game Create(Game entity)
      {
         string sql = "INSERT INTO GAME (Title," +
                              "Resume," +
                              "DateSortie) " +
         "OUTPUT INSERTED.*" +
         "VALUES (@title,@description,@date)";
         SqlParameter[] parameters =
         {
            GenerateParameter("title",entity.Title),
            GenerateParameter("description", entity.Resume),
            GenerateParameter("date", entity.DateDeSortie)
         };
         Game game = ExecuteReaderOneElement<Game>(sql, CommandType.Text, parameters, reader => MapperCreate(reader));
         AddGenreToGame(game.Id, entity.Genres.Id);
         return game;
      }

      public override bool Update(Game entity)
      {
         string sql = "UPDATE GAME SET Title = @title, Resume = @description, DateSortie = @date WHERE Id = @id";
         SqlParameter[] parameters =
         {
            GenerateParameter("title",entity.Title),
            GenerateParameter("description", entity.Resume),
            GenerateParameter("date", entity.DateDeSortie),
            GenerateParameter("id",entity.Id)
         };
         int row = ExecuteNonQuery(sql, CommandType.Text, parameters);
         sql = "UPDATE GAMEGENRE SET GenreId = @genreId WHERE GameId = @id";
         SqlParameter[] parameters2 =
         {
            GenerateParameter("id",entity.Id),
            GenerateParameter("genreId",entity.Genres.Id)
         };
         ExecuteNonQuery(sql, CommandType.Text, parameters2);
         return row != 0;
      }

      private void AddGenreToGame(int gameId, int genreId)
      {
         string sql = "INSERT INTO GAMEGENRE (GameId, GenreId) " +
                      "VALUES (@gameId, @genreId)";
         SqlParameter[] parameters =
         {
            GenerateParameter("gameId",gameId),
            GenerateParameter("genreId",genreId)
         };
         ExecuteNonQuery(sql, CommandType.Text, parameters);
      }

      public Dictionary<string, List<Game>> GamesByGenre()
      {
         Dictionary<string, List<Game>> listByGenre = new Dictionary<string, List<Game>>();
         foreach (Game g in ReadAll())
         {
            if (!listByGenre.ContainsKey(g.Genres.Label))
            {
               listByGenre.Add(g.Genres.Label, new List<Game>());
            }
            listByGenre[g.Genres.Label].Add(g);
         }
         return listByGenre;
      }
      public void AddGameToFavList(Guid userId,int gameId)
      {
         string sql = "INSERT INTO UserFavGames (UserId, GameId) " +
             "VALUES (@userId, @gameId)";
         SqlParameter[] parameters =
         {
            GenerateParameter("gameId",gameId),
            GenerateParameter("userId",userId)
         };
         ExecuteNonQuery(sql, CommandType.Text, parameters);
      }


   }
}
