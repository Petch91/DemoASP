using DAL;
using DAL.Interfaces;
using DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
   {

      private readonly HttpClient _client;
      private readonly string _url = "https://localhost:7079/api/";

      protected BaseRepository(HttpClient client)
      {
         _client = client;
         _client.BaseAddress = new Uri(_url);
      }

      public TResult Get<TResult>(string controllerName = "", string route ="", string token = "")
      {
         if (!string.IsNullOrWhiteSpace(token))
         {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
         }
         if (string.IsNullOrWhiteSpace(controllerName))
         {
            controllerName = typeof(TEntity).Name;
         }
         using (HttpResponseMessage response = _client.GetAsync(controllerName + (route is not null ? "/" + route : "")).Result)
         {
            TResult entity = default;
            try
            {
               if (response.IsSuccessStatusCode)
               {
                  string json = response.Content.ReadAsStringAsync().Result;
                  entity = JsonConvert.DeserializeObject<TResult>(json);
               }
               else throw new Exception("erreur");
               return entity;
            }
            catch (Exception)
            {

               throw new Exception(response.StatusCode.ToString());
            }

         }
      }




      public bool Delete(string controllerName = "", string route = "", string token = "")
      {
         if (!string.IsNullOrWhiteSpace(token))
         {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
         }
         if (string.IsNullOrWhiteSpace(controllerName))
         {
            controllerName = typeof(TEntity).Name;
         }
         using (HttpResponseMessage response = _client.DeleteAsync(controllerName + (route is not null ? "/" + route : "")).Result)
         {

               if (response.IsSuccessStatusCode)
               {
                  return true;
               }
               return false;

         }
      }


      public TResult Post<TResult>(object entity, string controllerName = "", string route = "", string token="")
      {
         HttpContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
         Console.WriteLine(content);
         if (!string.IsNullOrWhiteSpace(token))
         {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
         }
         if (string.IsNullOrWhiteSpace(controllerName))
         {
            controllerName = typeof(TEntity).Name;
         }
         using (HttpResponseMessage response = _client.PostAsync(controllerName + (route is not null ? "/" + route : ""), content).Result)
         {
            TResult entityReponse = default;
            try
            {
               if (response.IsSuccessStatusCode)
               {
                  string json = response.Content.ReadAsStringAsync().Result;
                  entityReponse = JsonConvert.DeserializeObject<TResult>(json);
                  return entityReponse;
               }
               else
               { throw new Exception(response.StatusCode.ToString()); }
            }
            catch (Exception)
            {

               throw new Exception(response.StatusCode.ToString());
            }
         }

      }
   }
}


//public abstract TEntity Create(TEntity entity);


//public TEntity ReadOne(TKey id, string controllerName = "")
//{
//   if (string.IsNullOrWhiteSpace(controllerName))
//   {
//      controllerName = typeof(TEntity).Name;
//   }
//   using (HttpResponseMessage response = _client.GetAsync(controllerName + "/" + id).Result)
//   {
//      TEntity entity = default;
//      try
//      {
//         if (response.IsSuccessStatusCode)
//         {
//            string json = response.Content.ReadAsStringAsync().Result;
//            entity = JsonConvert.DeserializeObject<TEntity>(json);
//         }
//         return entity;
//      }
//      catch (Exception)
//      {

//         throw new Exception(response.StatusCode.ToString());
//      }

//   }
//}
//public IEnumerable<TEntity> ReadAll(string controllerName = "")
//{
//   if (string.IsNullOrWhiteSpace(controllerName))
//   {
//      controllerName = typeof(TEntity).Name;
//   }
//   using (HttpResponseMessage response = _client.GetAsync(controllerName).Result)
//   {
//      IEnumerable<TEntity> entities = default;
//      try
//      {
//         if (response.IsSuccessStatusCode)
//         {
//            string json = response.Content.ReadAsStringAsync().Result;
//            entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(json);
//         }
//         return entities;
//      }
//      catch (Exception)
//      {

//         throw new Exception(response.StatusCode.ToString());
//      }

//   }
//}

//public abstract bool Update(TEntity entity);