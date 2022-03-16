using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Domain.Dtos;

namespace UlusalAjans.Data.Abstract
{
    public interface ICategoryService
    {
        CategoryDto GetById(int id);
        IEnumerable<CategoryDto> GetAll();
        CategoryDto Insert(CategoryDto category);
        void Update(CategoryDto category);
        void Delete(int id);
        bool IsExist(int id);
    }
}
