
using DAL.Interfaces;
using DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class GameRepository : BaseRepository<Game>, IGameRepository
   {
      public GameRepository(HttpClient httpClient) : base(httpClient)
      {
      }

      public Dictionary<string, List<Game>> GamesByGenre()
      {
         Dictionary<string, List<Game>> listByGenre = new Dictionary<string, List<Game>>();
         foreach (Game g in Get<IEnumerable<Game>>())
         {
            if (!listByGenre.ContainsKey(g.Genres.Label))
            {
               listByGenre.Add(g.Genres.Label, new List<Game>());
            }
            listByGenre[g.Genres.Label].Add(g);
         }
         return listByGenre;
      }
   }
}
