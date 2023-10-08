using DAL.Interfaces;
using DAL.Models;
using DAL.Models.Enums;

using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DAL
{
   public class UserRepository : BaseRepository<User>, IUserRepository
   {
      public UserRepository(HttpClient httpClient) : base(httpClient)
      {
      }

   }
}
