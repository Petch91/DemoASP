using DemoASP.Models.ViewModel;
using DemoASP.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoASP.Controllers
{
   public class UserController : Controller
   {
      private  readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
      {
         return View(_userService.ReadAll());
      }

      public IActionResult Create() 
      {
         return View();
      }
      [HttpPost]
      public IActionResult Create(UserRegisterForm user) 
      {
         _userService.Create(_userService.convert(user));
         return RedirectToAction("Index");
      }
   }
}
