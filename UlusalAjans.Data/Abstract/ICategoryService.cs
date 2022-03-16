using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Entities;

namespace UlusalAjans.Data.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);
        IEnumerable<Category> GetAll();
        Category Insert(Category category);
        void Update(Category category);
        void Delete(int id);
        bool IsExist(int id);
    }
}
