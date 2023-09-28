
using DemoASP.Models;
using DemoASP.Models.ViewModel;
using DemoASP.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoASP.Controllers
{
   public class UserController : Controller
   {
      private readonly IUserRepository _userRepository;
      public UserController(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }
      public IActionResult Index([FromRoute] Guid id)
      {
         
         return View(_userRepository.ReadOne(id));
      }

      public IActionResult Register()
      {
         return View();
      }
      [HttpPost]
      public IActionResult Register(UserRegisterForm user)
      {
         if (!ModelState.IsValid)
         {
            return View(user);
         }
         if (_userRepository.Register(user.Email, user.Password, user.Username))
         {
            return RedirectToAction("Index","Game");
         }
         return View();
      }
      public IActionResult Login()
      {
         return View();
      }
      [HttpPost]
      public IActionResult Login(UserLoginForm user)
      {
         if (!ModelState.IsValid)
         {
            return View();
         }
         try
         {
            User u = _userRepository.Login(user.Email, user.Password);
            return RedirectToAction("Index",new { id = u.Id });
         }
         catch(Exception ex) 
         {
            TempData["error"] = ex.Message;
            return View();
         }
      }
   }
}
