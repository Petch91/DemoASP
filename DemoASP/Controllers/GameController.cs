
using DAL.Interfaces;
using DAL.Models;
using DemoASP.Tools;
using Microsoft.AspNetCore.Mvc;

namespace DemoASP.Controllers
{
   [CustomAuthorize]
   public class GameController : Controller
   {
      private readonly IGameRepository _gameRepository;
      private readonly SessionManager _session;
      public GameController(IGameRepository gameRepository, SessionManager session)
      {
         _gameRepository = gameRepository;
         _session = session;
      }
      public IActionResult Index()
      {
         return View(_gameRepository.Get<IEnumerable<Game>>());
      }
      public IActionResult Details(int id)
      {
         return View(_gameRepository.Get<Game>(route: $"{id}"));
      }
      [CustomAdmin]
      public IActionResult Create()
      {
         return View();
      }
      [HttpPost]
      public IActionResult Create(Game g)
      {
         _gameRepository.Post<Game>(new {g.Title,IdGenre = g.Genres.Id,g.Resume},route:"add", token:_session.ConnectedUser.Token);
         return RedirectToAction("Index");
      }
      [CustomAdmin]
      public IActionResult Delete(int id)
      {
         if (_gameRepository.Delete(route: $"{id}")) return RedirectToAction("Index");
         return View();
      }
      [CustomModo]
      public IActionResult Edit(int id)
      {
         return View();
      }
      [HttpPost]
      public IActionResult Edit(Game g)
      {
         //_gameRepository.Update(g);
         return View();
      }

      public IActionResult ListByGenre()
      {
         return View(_gameRepository.Get<Dictionary<string,Game>>(route:"byGenre"));
      }
      public IActionResult AddFavGame(int id)
      {
         _gameRepository.Post<object>(null,route:$"addFavori/{id}/user/{_session.ConnectedUser.Id}", token: _session.ConnectedUser.Token);
         return RedirectToAction("Index");
      }

      public IActionResult FavList(Guid id)
      {
         return View(_gameRepository.Get<IEnumerable<Game>>(route: $"Favoris/{id}",token: _session.ConnectedUser.Token));
      }
   }
}