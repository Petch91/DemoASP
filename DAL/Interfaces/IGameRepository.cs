using BibliothequeDAL.Repos;
using DemoASP.Models;

namespace DemoASP.Services.Interfaces
{
   public interface IGameRepository : IBaseRepository<int,Game>
   {
   }
}
