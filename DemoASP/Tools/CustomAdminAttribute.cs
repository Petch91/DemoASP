using DAL.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoASP.Tools
{
   public class CustomAdminAttribute : TypeFilterAttribute
   {
      public CustomAdminAttribute() : base(typeof(AdminRequiredFilter))
      {
      }
   }
   public class AdminRequiredFilter : IAuthorizationFilter
   {
      private readonly SessionManager _session;
      public AdminRequiredFilter(SessionManager session)
      {
         _session = session;
      }
      public void OnAuthorization(AuthorizationFilterContext context)
      {
         if (_session.ConnectedUser.Role != Role.Admin )
         {
            context.Result = new RedirectToRouteResult(new { action = "NotAuth", Controller = "Home" });
         }
      }
   }
}
