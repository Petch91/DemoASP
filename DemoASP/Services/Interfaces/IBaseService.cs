using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequeDAL.Repos
{
   public interface IBaseService<TKey,TEntity> where TEntity : class
   {
      TEntity Create(TEntity entity);
      TEntity ReadOne(TKey id);
      IEnumerable<TEntity> ReadAll();
      bool Update(TEntity entity);
      bool Delete(TKey id);
   }
}
