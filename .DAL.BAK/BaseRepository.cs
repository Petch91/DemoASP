using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public abstract class BaseRepository<TKey, TEntity> : DBService, IBaseRepository<TKey, TEntity> where TEntity : class
   {

      //private string _connectionString = @"Data Source=DESKTOP-9B27V2B;Initial Catalog=GameDB2;Integrated Security=True;";
      private string _table;
      private string _idColName;

      protected BaseRepository(SqlConnection cnx, string table, string idCol) : base(cnx)
      {
         _table = table;
         _idColName = idCol;
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
         TEntity entity = ExecuteReaderOneElement<TEntity>(sql, CommandType.Text, parameters, reader => Mapper(reader));
         return entity;
      }

      public IEnumerable<TEntity> ReadAll()
      {
         string sql = "SELECT * FROM " + _table;

         IEnumerable<TEntity> entities = ExecuteReader<TEntity>(sql, CommandType.Text, reader => Mapper(reader));
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
         return ExecuteNonQuery(sql, CommandType.Text, parameters) != 0;
      }
   }
}
