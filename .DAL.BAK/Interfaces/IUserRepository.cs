using DAL.Models;

namespace DAL.Interfaces
{
   public interface IUserRepository : IBaseRepository<Guid, User>
   {

      bool Register(string email, string password, string username);

      User Login(string email, string pwd);
      IEnumerable<Game> GetFavGames(Guid userId);
   }
}
