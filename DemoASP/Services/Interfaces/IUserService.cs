using BibliothequeDAL.Repos;
using DemoASP.Models;
using DemoASP.Models.ViewModel;

namespace DemoASP.Services.Interfaces
{
   public interface IUserService : IBaseService<Guid,User>
   {
      public User convert(UserRegisterForm user);
   }
}
