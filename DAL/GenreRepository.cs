using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class GenreRepository : BaseRepository<int, Genre>, IGenreRepository
   {
      public GenreRepository(SqlConnection cnx) : base(cnx, "GENRE", "GenreId")
      {
      }

      public override Genre Create(Genre entity)
      {
         string sql = "INSERT INTO GENRE (Label) " +
                      "OUTPUT INSERTED.* " +
                      "VALUES (@label)";
         SqlParameter[] parameters =
         {
            GenerateParameter("label",entity.Label)
         };
         return ExecuteReaderOneElement<Genre>(sql, CommandType.Text, parameters, reader => Mapper(reader));
      }

      public override Genre Mapper(SqlDataReader record)
      {
         return new Genre { Id = (int)record["GenreId"], Label = (string)record["Label"] };
      }

      public override bool Update(Genre entity)
      {
         throw new NotImplementedException();
      }
   }
}
