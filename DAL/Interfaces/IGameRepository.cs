
using DAL.Models;

namespace DAL.Interfaces
{
   public interface IGameRepository : IBaseRepository<int,Game>
   {
      Dictionary<string, List<Game>> GamesByGenre();
      void AddGameToFavList(Guid userId, int gameId);
   }
}
