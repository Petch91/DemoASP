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

         return View(_userRepository.Get<User>(route: id.ToString(), token: _session.ConnectedUser.Token));
      }

      public IActionResult List()
      {
         return View(_userRepository.Get<IEnumerable<User>>().Select(u => u.ToUserView()));
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
         _userRepository.Post<User>(new { user.Email, user.Password, user.Username }, route: "register");

         return RedirectToAction("Index", "Home");
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
            User u = _userRepository.Post<User>(new { user.Email, user.Password },route: "login");
            _session.ConnectedUser = u;
            return RedirectToAction("Index", new { id = _session.ConnectedUser.Id });
         }
         catch (Exception ex)
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

   }
}
