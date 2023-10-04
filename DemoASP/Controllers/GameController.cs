
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
         return View(_gameRepository.ReadAll());
      }
      public IActionResult Details(int id)
      {
         return View(_gameRepository.ReadOne(id));
      }
      [CustomAdmin]
      public IActionResult Create()
      {
         return View();
      }
      [HttpPost]
      public IActionResult Create(Game g)
      {
         _gameRepository.Create(g);
         return RedirectToAction("Index");
      }
      [CustomAdmin]
      public IActionResult Delete(int id)
      {
         if (_gameRepository.Delete(id)) return RedirectToAction("Index");
         return View();
      }
      [CustomModo]
      public IActionResult Edit(int id)
      {
         return View(_gameRepository.ReadOne(id));
      }
      [HttpPost]
      public IActionResult Edit(Game g)
      {
         _gameRepository.Update(g);
         return View();
      }

      public IActionResult ListByGenre()
      {
         return View(_gameRepository.GamesByGenre());
      }
      public IActionResult AddFavGame(int id)
      {
         _gameRepository.AddGameToFavList(_session.ConnectedUser.Id, id);
         return RedirectToAction("Index");
      }
   }
}