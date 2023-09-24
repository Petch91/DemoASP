using DemoASP.Models.Enum;

namespace DemoASP.Models
{
   public class User
   {
      public Guid Id { get; set; }
      public string UserName { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
      public Role Role { get; set; }

   }
}
