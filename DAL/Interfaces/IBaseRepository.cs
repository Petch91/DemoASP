using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
   public interface IBaseRepository<TEntity> where TEntity : class
   {

      TResult Get<TResult>( string controllerName = "", string route = "", string token = "");
      TResult Post<TResult>(object Entity, string controllerName = "", string route = "", string token = "");
      //TEntity Put(TEntity Entity, string controllerName, string route, string token);
      //bool Patch(TEntity Entity, string controllerName, string route, string token);
      bool Delete(string controllerName = "", string route = "", string token = "");
   }
}

//TEntity Create(TEntity entity);
//TEntity ReadOne(TKey id, string controllerName);
//IEnumerable<TEntity> ReadAll(string controllerName);
//bool Update(TEntity entity);