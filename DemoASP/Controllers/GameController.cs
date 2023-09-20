using DemoASP.Models;
using DemoASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DemoASP.Controllers
{
    public class GameController : Controller
   {
        private readonly GameDbService _gameService;
        public GameController(GameDbService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
      {
         return View(_gameService.ReadAll());
      }
      public IActionResult Details(int id)
      {
         return View(_gameService.ReadOne(id));
      }
      public IActionResult Create()
      {
         return View();
      }
      [HttpPost]
      public IActionResult Create(Game g)
      {
         _gameService.Create(g);
         return RedirectToAction("Index");
      }

      public IActionResult Delete(int id) 
      {
         if(_gameService.Delete(id)) return RedirectToAction("Index");
         return View();
      }
      public IActionResult Edit( int id)
      {
         return View(_gameService.ReadOne(id));
      }
      [HttpPost]
      public IActionResult Edit(Game g)
      {
         _gameService.Update(g);
         return View();
      }
   }
}