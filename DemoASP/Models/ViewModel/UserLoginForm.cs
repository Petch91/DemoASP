using System.ComponentModel.DataAnnotations;

namespace DemoASP.Models.ViewModel
{
   public class UserLoginForm
   {

      [Required]
      [EmailAddress]
      public string Email { get; set; }
      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }
   }
}
