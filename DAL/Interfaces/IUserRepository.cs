using BibliothequeDAL.Repos;
using DemoASP.Models;

namespace DemoASP.Services.Interfaces
{
   public interface IUserRepository : IBaseRepository<Guid,User>
   {

      bool Register(string email, string password, string username);

      User Login(string email, string pwd);
   }
}
