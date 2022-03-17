using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using UlusalAjans.Data.Abstract;
using UlusalAjans.Domain.Dtos;

namespace UlusalAjans.Data.Dapper
{
    public class DapperCategoryService : ICategoryService
    {
        private readonly SqlConnection _connection;

        public DapperCategoryService(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("NorthwindConnStr"));
        }

        public void Delete(int id)
        {
            var query = "delete from Categories where Id=@id";
            _connection.Execute(query, new { @Id = id });
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var query = "select * from Categories";
            return _connection.Query<CategoryDto>(query);
        }

        public CategoryDto GetById(int id)
        {
            var query = "select * from Categories where Id=@id";
            return _connection.QuerySingle<CategoryDto>(query, new { @id = id });
        }

        public CategoryDto Insert(CategoryDto category)
        {
            

            var query = "insert into Categories (Name,Description) values (@name,@description);select scope_identity()";
            var id = _connection.ExecuteScalar<int>(query, new { @name = category.Name, @description = category.Description });
            category.Id = id;
            return category;
        }

        public bool IsExist(int id)
        {
            var query = "select count(*) from Categories where Id=@id";
            var count = _connection.ExecuteScalar<int>(query, new { @id = id });
            return count > 0;
        }

        public void Update(CategoryDto category)
        {
            var query = "update Categories set Name=@name, Description=@description where Id=@id)";
            _connection.Execute(query, new { @name = category.Name, @description = category.Description, @id = category.Id });
        }
    }
}
