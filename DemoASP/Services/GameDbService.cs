﻿using DemoASP.Models;
using GamesDataAccessLayer.Services;
using System.Data;
using System.Data.SqlClient;

namespace DemoASP.Services
{
   public class GameDbService : Service<int, Game>
   {
      public GameDbService() : base("Game", "Id")
      {
      }

      public override Game Convert(SqlDataReader record)
      {
         return new Game
         {
            Id = (int)record["Id"],
            Title = (string)record["Title"],
            Resume = record["Resume"] == DBNull.Value ? null : (string)record["Resume"],
            DateDeSortie = (DateTime)record["DateSortie"],
            Genre = (string)record["Genre"]
         };
      }

      public override Game Create(Game entity)
      {
         string sql = "INSERT INTO GAME (Title," +
                              "Resume," +
                              "DateSortie," +
                              "Genre) " +
         "OUTPUT INSERTED.*" +
         "VALUES (@title,@description,@date,@genre)";
         SqlParameter[] parameters =
         {
            GenerateParameter("title",entity.Title),
            GenerateParameter("description", entity.Resume),
            GenerateParameter("date", entity.DateDeSortie),
            GenerateParameter("genre", entity.Genre)
         };
         IEnumerable<Game> games = ExecuteReader<Game>(sql, parameters, reader => Convert(reader));
         return games.Count() > 0 ? games.First() : new Game();
      }

      public override bool Update(Game entity)
      {
         string sql = "UPDATE GAME SET Title = @title, Resume = @description, DateSortie = @date, Genre = @genre WHERE Id = @id";
         SqlParameter[] parameters =
         {
            GenerateParameter("title",entity.Title),
            GenerateParameter("description", entity.Resume),
            GenerateParameter("genre", entity.Genre),
            GenerateParameter("date", entity.DateDeSortie),
            GenerateParameter("id",entity.Id)
         };
         return ExecuteNonQuery(sql, parameters) != 0;
      }
   }
}