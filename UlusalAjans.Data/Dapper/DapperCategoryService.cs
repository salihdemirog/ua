using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data.Abstract;

namespace UlusalAjans.Data.Dapper
{
    public class DapperCategoryService : ICategoryService
    {
        private readonly SqlConnection _connection;

        public DapperCategoryService(SqlConnection connection)
        {
            _connection = connection;
        }

        public void Delete(int id)
        {
            var query = "delete from Categories where Id=@id";

            _connection.Execute(query, new { @id = id });
        }

        public IEnumerable<Category> GetAll()
        {
            var query = "select * from Categories";

            return _connection.Query<Category>(query);
        }

        public Category GetById(int id)
        {
            var query = "select * from Categories where Id=@id";
            return _connection.QuerySingle<Category>(query, new { @id = id });
        }

        public Category Insert(Category category)
        {
            var query = "insert into Categories (Name,Description) values (@name,@description); select scope_identity()";

            var id = _connection.ExecuteScalar<int>(query, 
                new { @name = category.Name, @description = category.Description });

            category.Id = id;

            return category;
        }

        public bool IsExist(int id)
        {
            var query = "select count(*) from Categories where Id=@id";
            var count = _connection.ExecuteScalar<int>(query, new { @id = id });
            return count > 0;
        }

        public void Update(Category category)
        {
            var query = "update Categories set Name=@name, Description=@description where Id=@id";

            _connection.Execute(query, new { @name = category.Name, @description = category.Description, @id = category.Id });
        }
    }
}
