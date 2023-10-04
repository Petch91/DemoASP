using DAL.Models.Enums;

namespace DemoASP.Models.ViewModel
{
   public class UserView
   {
      public Guid Id { get; set; }
      public string UserName { get; set; }
      public Role Role { get; set; }
   }
}
