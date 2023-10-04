using DAL.Interfaces;
using DAL.Models;
using DemoASP.Tools;
using Microsoft.AspNetCore.Mvc;

namespace DemoASP.Controllers
{
   [CustomAdmin]
   public class GenreController : Controller
   {
      private readonly IGenreRepository _genreRepository;
      public GenreController(IGenreRepository genreRepository)
      {
         _genreRepository = genreRepository;
      }
      public IActionResult List()
      {
         return View(_genreRepository.ReadAll());
      }

      public IActionResult Add() 
      {
         return View();
      }
      [HttpPost]
      public IActionResult Add(Genre g)
      {
         if (!ModelState.IsValid) 
         {
            return View(g);
         }
         _genreRepository.Create(g);
         return RedirectToAction("List");
      }

      public IActionResult Delete(int id) 
      {
         _genreRepository.Delete(id);
         return RedirectToAction("List");
      }
   }
}
