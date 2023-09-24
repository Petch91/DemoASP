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
      private SqlConnection _cnx;

      protected SqlCommand _cmd;

      public Service(string table, string idcolname)
      {
         _table = table;
         _idColName = idcolname;
         _cnx = new SqlConnection(_connectionString);
      }

      protected int ExecuteNonQuery(string sqlRequest)
      {
         int row;
         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cnx.Open();
            row = _cmd.ExecuteNonQuery();
            _cnx.Close();
         }
         return row;
      }

      protected int ExecuteNonQuery(string sqlRequest, SqlParameter[] parameters)
      {
         int row;
         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.Parameters.AddRange(parameters);
            _cnx.Open();
            row = _cmd.ExecuteNonQuery();
            _cnx.Close();
         }

         return row;
      }
      protected object ExecuteScalar(string sqlRequest)
      {
         object result;
         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cnx.Open();
            result = _cmd.ExecuteScalar();
            _cnx.Close();
         }

         return result;
      }
      protected object ExecuteScalar(string sqlRequest, SqlParameter[] parameters)
      {
         object result;

         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.Parameters.AddRange(parameters);
            _cnx.Open();
            result = _cmd.ExecuteScalar();
            _cnx.Close();

            return result;
         }
      }
      protected List<T> ExecuteReader<T>(string sqlRequest, Func<SqlDataReader, T> mapper)
      {
         List<T> list = new List<T>();

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
         return list;
      }
      protected List<T> ExecuteReader<T>(string sqlRequest, SqlParameter[] parameters, Func<SqlDataReader, T> mapper)
      {
         List<T> list = new List<T>();

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
         return list;
      }
      protected T ExecuteReaderOneElement<T>(string sqlRequest, Func<SqlDataReader, T> mapper)
      {
         T t = default(T);

         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cnx.Open();
            using (SqlDataReader reader = _cmd.ExecuteReader())
            {
               if (reader.Read())
               {
                  t = mapper(reader);
               }
            }
            _cnx.Close();
         }
         return t;
      }
      protected T ExecuteReaderOneElement<T>(string sqlRequest, SqlParameter[] parameters, Func<SqlDataReader, T> mapper)
      {
         T t  = default(T);

         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.Parameters.AddRange(parameters);
            _cnx.Open();
            using (SqlDataReader reader = _cmd.ExecuteReader())
            {
               if (reader.Read())
               {
                  t = mapper(reader);
               }
            }
            _cnx.Close();
         }
         return t;
      }
      protected SqlParameter GenerateParameter(string name, object value)
      {
         return new SqlParameter(name, value ?? DBNull.Value);
      }
      public abstract TEntity Mapper(SqlDataReader record);

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
         TEntity entity = ExecuteReaderOneElement<TEntity>(sql, parameters, reader => Mapper(reader));
         return entity;
      }

      public IEnumerable<TEntity> ReadAll()
      {
         string sql = "SELECT * FROM " + _table;

         IEnumerable<TEntity> entities = ExecuteReader<TEntity>(sql, reader => Mapper(reader));
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
