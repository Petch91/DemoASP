using BibliothequeDAL.Repos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesDataAccessLayer.Services
{
   public abstract class Service<TKey, TEntity> : IBaseService<TKey, TEntity> where TEntity : class
   {

      private string _connectionString = @"Data Source=DESKTOP-T1P11FV;Initial Catalog=GameDB2;Integrated Security=True;";
      private string _table;
      private string _idColName;
      protected SqlConnection _cnx { get; set; }

      protected SqlConnection CreateConnection()
      {
         return new SqlConnection(_connectionString);
      }

      protected SqlCommand _cmd;

      public Service(string table, string idcolname)
      {
         _table = table;
         _idColName = idcolname;
      }
      
      protected int ExecuteNonQuery(string sqlRequest)
      {
         int row;
         using (_cnx = CreateConnection())
         {
            using (_cmd = _cnx.CreateCommand())
            {
               _cmd.CommandText = sqlRequest;
               _cnx.Open();
               row = _cmd.ExecuteNonQuery();
               _cnx.Close();
            }
         }
         return row;
      }

      protected int ExecuteNonQuery(string sqlRequest, SqlParameter[] parameters)
      {
         int row;
         using (_cnx = CreateConnection())
         {
            using (_cmd = _cnx.CreateCommand())
            {
               _cmd.CommandText = sqlRequest;
               _cmd.Parameters.AddRange(parameters);
               _cnx.Open();
               row = _cmd.ExecuteNonQuery();
               _cnx.Close();
            }
         }
         return row;
      }
      protected object ExecuteScalar(string sqlRequest)
      {
         object result;
         using (_cnx = CreateConnection())
         {
            using (_cmd = _cnx.CreateCommand())
            {
               _cmd.CommandText = sqlRequest;
               _cnx.Open();
               result = _cmd.ExecuteScalar();
               _cnx.Close();
            }
         }
         return result;
      }
      protected object ExecuteScalar(string sqlRequest, SqlParameter[] parameters)
      {
         object result;
         using (_cnx = CreateConnection())
         {
            using (_cmd = _cnx.CreateCommand())
            {
               _cmd.CommandText = sqlRequest;
               _cmd.Parameters.AddRange(parameters);
               _cnx.Open();
               result = _cmd.ExecuteScalar();
               _cnx.Close();
            }
         }
         return result;
      }
      protected List<T> ExecuteReader<T>(string sqlRequest, Func<SqlDataReader, T> mapper)
      {
         List<T> list = new List<T>();
         using (_cnx = CreateConnection())
         {
            using (_cmd = _cnx.CreateCommand())
            {
               _cmd.CommandText = sqlRequest;
               _cnx.Open();
               using (SqlDataReader reader = _cmd.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     list.Add(mapper(reader));
                  }
               }
               _cnx.Close();
            }
         }
         return list;
      }
      protected List<T> ExecuteReader<T>(string sqlRequest, SqlParameter[] parameters, Func<SqlDataReader, T> mapper)
      {
         List<T> list = new List<T>();
         using (_cnx = CreateConnection())
         {
            using (_cmd = _cnx.CreateCommand())
            {
               _cmd.CommandText = sqlRequest;
               _cmd.Parameters.AddRange(parameters);
               _cnx.Open();
               using (SqlDataReader reader = _cmd.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     list.Add(mapper(reader));
                  }
               }
               _cnx.Close();
            }
         }
         return list;
      }
      protected SqlParameter GenerateParameter(string name, object value)
      {
         return new SqlParameter(name, value ?? DBNull.Value);
      }
      public abstract TEntity Convert(SqlDataReader record);

      public abstract TEntity Create(TEntity entity);


      public TEntity ReadOne(TKey id)
      {
         string sql = "SELECT * FROM " +
                        _table +
                      " WHERE " +
                        _idColName +
                      " = @id";
         SqlParameter[] parameters =
         {
            GenerateParameter("id",id),
         };
         IEnumerable<TEntity> entities = ExecuteReader<TEntity>(sql, parameters, reader => Convert(reader));
         return entities.Count() > 0 ? entities.First() : null;
      }

      public IEnumerable<TEntity> ReadAll()
      {
         string sql = "SELECT * FROM " + _table;

         IEnumerable<TEntity> entities = ExecuteReader<TEntity>(sql, reader => Convert(reader));
         return entities;
      }

      public abstract bool Update(TEntity entity);


      public bool Delete(TKey id)
      {
         string sql = "DELETE FROM " +
               _table +
             " WHERE " +
               _idColName +
             " = @id";
         SqlParameter[] parameters =
         {
            GenerateParameter("id",id),
         };
         return ExecuteNonQuery(sql, parameters) != 0;
      }
   }
}
