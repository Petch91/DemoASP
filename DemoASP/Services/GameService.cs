using DemoASP.Models;
using System.Linq;

namespace DemoASP.Services
{
   public class GameService
   {
      private List<Game> _games = new List<Game>();
      private static int _id = 1;
      public GameService()
      {
         _games.Add(new Game { Id = _id, DateDeSortie = new DateTime(2003, 10, 19), Title = "Call Of Noob", Genre = "fps", Resume = "c est un petit jeux de tir" });
         _id++;
         _games.Add(new Game { Id = _id, DateDeSortie = new DateTime(2001, 05, 10), Title = "Final Fantasy 9", Genre = "rpg", Resume = "Best Game" });
         _id++;
         _games.Add(new Game { Id = _id, DateDeSortie = new DateTime(1995, 03, 05), Title = "Fallout 2", Genre = "Aventure", Resume = "jeu de bethesda" });
      }

      public List<Game> GetList()
      {

         return _games;
      }

      public Game GetGameByid(int id)
      {
         return _games.FirstOrDefault(g => g.Id == id);
      }
      public void AddGame(Game game)
      {
         _id++;
         game.Id = _id;
         _games.Add(game);
      }

      public void RemoveGame(int id)
      {
         _games.Remove(_games.FirstOrDefault(g => g.Id == id));
      }

      public void EditGame(Game game)
      {
         int i = _games.IndexOf(_games.FirstOrDefault(g => g.Id == game.Id));
         _games.Remove(_games.FirstOrDefault(g => g.Id == game.Id));
         _games.Insert(i, game);
      }
   }
}
