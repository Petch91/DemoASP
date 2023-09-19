using DemoASP.Models;
using DemoASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DemoASP.Controllers
{
    public class GameController : Controller
   {
        private readonly GameService _gameService;
        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
      {
         return View(_gameService.GetList());
      }
      public IActionResult Details(int id)
      {
         return View(_gameService.GetGameByid(id));
      }
      public IActionResult Create()
      {
         return View();
      }
      [HttpPost]
      public IActionResult Create(Game g)
      {
         _gameService.AddGame(g);
         return RedirectToAction("Index");
      }

      public IActionResult Delete(int id) 
      {
         _gameService.RemoveGame(id);
         return View();
      }
      public IActionResult Edit( int id)
      {
         return View(_gameService.GetGameByid(id));
      }
      [HttpPost]
      public IActionResult Edit(Game g)
      {
         _gameService.EditGame(g);
         return View();
      }
   }
}