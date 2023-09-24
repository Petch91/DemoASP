using DemoASP.Models;
using DemoASP.Models.Enum;
using DemoASP.Models.ViewModel;
using DemoASP.Services.Interfaces;
using GamesDataAccessLayer.Services;
using System.Data.SqlClient;

namespace DemoASP.Services
{
   public class UserService : Service<Guid, User>, IUserService
   {
      public UserService() : base("[USER]", "Id")
      {
      }

      public User convert(UserRegisterForm user)
      {
         return new User { UserName = user.Username, 
                           Email = user.Email,
                           Password = user.Password};
      }

      public override User Create(User entity)
      {
         string sql =   "INSERT INTO [User] (Username," +
                        "Email," +
                        "Password) " +
                        "OUTPUT INSERTED.*" +
                        " VALUES (@name,@email,@password)";
         SqlParameter[] parameters =
         {
            GenerateParameter("name",entity.UserName),
            GenerateParameter("email", entity.Email),
            GenerateParameter("password", entity.Password)
         };
         IEnumerable<User> users = ExecuteReader<User>(sql, parameters, reader => Mapper(reader));
         return users.Count() > 0 ? users.First() : new User();
      }

      public override User Mapper(SqlDataReader record)
      {
         return new User { Id = (Guid)record["Id"],
                           UserName = (string)record["Username"],
                           Password = (string)record["Password"],
                           Email = (string)record["Email"],
                           Role = (Role)(int)record["Role"]
         };
      }

      public override bool Update(User entity)
      {
         throw new NotImplementedException();
      }
   }
}
