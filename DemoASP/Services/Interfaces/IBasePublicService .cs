using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequeDAL.Repos
{
   public interface IBasePublicService<TKey,TEntity> where TEntity : class
   {
      TEntity ReadOne(TKey id);
      IEnumerable<TEntity> ReadAll();

   }
}
