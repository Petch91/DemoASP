using DAL.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoASP.Tools
{
   public class CustomModoAttribute : TypeFilterAttribute
   {
      public CustomModoAttribute() : base(typeof(ModoRequiredFilter))
      {
      }
   }
   public class ModoRequiredFilter : IAuthorizationFilter
   {
      private readonly SessionManager _session;
      public ModoRequiredFilter(SessionManager session)
      {
         _session = session;
      }
      public void OnAuthorization(AuthorizationFilterContext context)
      {
         if (_session.ConnectedUser.Role != Role.Modo && _session.ConnectedUser.Role != Role.Admin)
         {
            context.Result = new RedirectToRouteResult(new { action = "NotAuth", Controller = "Home" });
         }
      }
   }
}
