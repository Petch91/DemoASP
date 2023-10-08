using DAL.Models;
using DemoASP.Models.ViewModel;

namespace DemoASP.Models.Mappers
{
   public static class Mappers
   {

      public static UserView ToUserView( this User user)
      {
         return new UserView { Id = user.Id, Role = user.Role, UserName = user.UserName };
      }
   }
}
