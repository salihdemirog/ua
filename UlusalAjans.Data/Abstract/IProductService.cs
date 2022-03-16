using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Dtos;
using UIusalAjans.Domain.Entities;

namespace UlusalAjans.Data.Abstract
{
    public interface IProductService
    {
        ProductDto GetById(int id);    

        IEnumerable<ProductDto> GetAll();

        ProductDto Insert(ProductDto product);

        void Update(ProductDto product);

        void Delete(int id);

        bool IsExist(int id);
    }
}
