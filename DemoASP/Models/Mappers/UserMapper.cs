using DAL.Models;
using DemoASP.Models.ViewModel;

namespace DemoASP.Models.Mappers
{
   public static class UserMapper
   {

      public static UserView ToUserView(User user)
      {
         return new UserView { Id = user.Id, Role = user.Role, UserName = user.UserName };
      }
   }
}
