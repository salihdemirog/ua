using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Entities;

namespace UlusalAjans.Data.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);    

        IEnumerable<Product> GetAll();

        Product Insert(Product product);

        void Update(Product product);

        void Delete(int id);

        bool IsExist(int id);
    }
}
