using DemoASP.Models;
using DemoASP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoASP.Controllers
{
   public class GameController : Controller
   {
        private readonly IGameRepository _gameRepository;
        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public IActionResult Index()
      {
         return View(_gameRepository.ReadAll());
      }
      public IActionResult Details(int id)
      {
         return View(_gameRepository.ReadOne(id));
      }
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

      public IActionResult Delete(int id) 
      {
         if(_gameRepository.Delete(id)) return RedirectToAction("Index");
         return View();
      }
      public IActionResult Edit( int id)
      {
         return View(_gameRepository.ReadOne(id));
      }
      [HttpPost]
      public IActionResult Edit(Game g)
      {
         _gameRepository.Update(g);
         return View();
      }
   }
}