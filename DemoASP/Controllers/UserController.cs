using DAL.Interfaces;
using DAL.Models;
using DemoASP.Models.Mappers;
using DemoASP.Models.ViewModel;
using DemoASP.Tools;
using Microsoft.AspNetCore.Mvc;

namespace DemoASP.Controllers
{
   public class UserController : Controller
   {
      private readonly IUserRepository _userRepository;
      private readonly SessionManager _session;
      public UserController(IUserRepository userRepository, SessionManager session)
      {
         _userRepository = userRepository;
         _session = session;
      }
      public IActionResult Index(Guid id)
      {
         
         return View(_userRepository.ReadOne(id));
      }

      public IActionResult List()
      {
         return View(_userRepository.ReadAll().Select(u => UserMapper.ToUserView(u)));
      }

      public IActionResult ChangeRole(Guid id)
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
            _session.ConnectedUser = u;
            return RedirectToAction("Index",new { id = _session.ConnectedUser.Id });
         }
         catch(Exception ex) 
         {
            TempData["error"] = ex.Message;
            return View();
         }
      }

      public IActionResult Logout()
      {
         _session.Logout();
         return RedirectToAction("Index", "Home");
      }

      public IActionResult FavList(Guid id) 
      {
         return View(_userRepository.GetFavGames(id));
      }
   }
}
