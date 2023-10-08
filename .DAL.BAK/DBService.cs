using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DBService
   {
      protected SqlConnection _cnx;

      protected SqlCommand _cmd;

      public DBService(SqlConnection cnx)
      {
         _cnx = cnx;
      }

      protected int ExecuteNonQuery(string sqlRequest,CommandType cmdType)
      {
         int row;
         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.CommandType = cmdType;
            _cnx.Open();
            row = _cmd.ExecuteNonQuery();
            _cnx.Close();
         }
         return row;
      }

      protected int ExecuteNonQuery(string sqlRequest, CommandType cmdType, SqlParameter[] parameters)
      {
         int row;
         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.CommandType = cmdType;
            _cmd.Parameters.AddRange(parameters);
            _cnx.Open();
            row = _cmd.ExecuteNonQuery();
            _cnx.Close();
         }

         return row;
      }
      protected object ExecuteScalar(string sqlRequest, CommandType cmdType)
      {
         object result;
         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.CommandType = cmdType;
            _cnx.Open();
            result = _cmd.ExecuteScalar();
            _cnx.Close();
         }

         return result;
      }
      protected object ExecuteScalar(string sqlRequest, CommandType cmdType, SqlParameter[] parameters)
      {
         object result;

         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.CommandType = cmdType;
            _cmd.Parameters.AddRange(parameters);
            _cnx.Open();
            result = _cmd.ExecuteScalar();
            _cnx.Close();

            return result;
         }
      }
      protected List<T> ExecuteReader<T>(string sqlRequest, CommandType cmdType, Func<SqlDataReader, T> mapper)
      {
         List<T> list = new List<T>();

         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.CommandType = cmdType;
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
      protected List<T> ExecuteReader<T>(string sqlRequest, CommandType cmdType, SqlParameter[] parameters, Func<SqlDataReader, T> mapper)
      {
         List<T> list = new List<T>();

         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.CommandType = cmdType;
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
      protected T ExecuteReaderOneElement<T>(string sqlRequest, CommandType cmdType, Func<SqlDataReader, T> mapper)
      {
         T t = default(T);

         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.CommandType = cmdType;
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
      protected T ExecuteReaderOneElement<T>(string sqlRequest, CommandType cmdType, SqlParameter[] parameters, Func<SqlDataReader, T> mapper)
      {
         T t = default(T);

         using (_cmd = _cnx.CreateCommand())
         {
            _cmd.CommandText = sqlRequest;
            _cmd.CommandType = cmdType;
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

   }
}
