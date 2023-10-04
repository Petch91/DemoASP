using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Enums;

using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DAL
{
   public class UserRepository : BaseRepository<Guid, User>, IUserRepository
   {
      public UserRepository(SqlConnection cnx) : base(cnx,"[USERS]", "Id")
      {
      }

      public bool Register(string email, string password, string username)
      {
         string sql = "UserRegister";
         SqlParameter[] parameters =
         {
            GenerateParameter("email",email),
            GenerateParameter("username", username),
            GenerateParameter("password", password)
         };
         return ExecuteNonQuery(sql,CommandType.StoredProcedure,parameters) > 0;
      }

      public override User Mapper(SqlDataReader record)
      {
         return new User { Id = (Guid)record["Id"],
                           UserName = (string)record["Username"],
                           Email = (string)record["Email"],
                           RoleId = (int)record["RoleId"],
                           Role = (Role)(int)record["RoleId"]
         };
      }

      public User Login(string email, string pwd)
      {
         string sql = "UserLogin";
         SqlParameter[] parameters =
         {
            GenerateParameter("email",email),
            GenerateParameter("pwd", pwd)
         };
         User u = ExecuteReaderOneElement<User>(sql, CommandType.StoredProcedure, parameters, reader => Mapper(reader));
         if(u is null) 
         {
            throw new Exception("Login Failed: Email or password is wrong");
         }
         return u;

      }
      public IEnumerable<Game> GetFavGames(Guid userId)
      {
         string sql = "SELECT Id, Title, [Resume], DateSortie ,GenreId,[Label] FROM UserFavView WHERE UserId = @userId";
         SqlParameter[] parameters =
         {
            GenerateParameter("userId",userId)
         };
         IEnumerable<Game> lg = ExecuteReader<Game>(sql, CommandType.Text, parameters, reader => new Game
         {
            Id = (int)reader["Id"],
            Title = (string)reader["Title"],
            Resume = reader["Resume"] == DBNull.Value ? null : (string)reader["Resume"],
            Genres = new Genre { Id = (int)reader["GenreId"], Label = (string)reader["Label"] },
            DateDeSortie = (DateTime)reader["DateSortie"]
         });
         if (lg is null)
         {
            lg = new List<Game>();
         }
         return lg;
      }

      public override bool Update(User entity)
      {
         throw new NotImplementedException();
      }

      public override User Create(User entity)
      {
         throw new NotImplementedException();
      }
   }
}
