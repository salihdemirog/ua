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
        Task<ProductDetailDto> GetByIdAsync(int id);

        Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<ProductDto> InsertAsync(ProductDto product);

        Task UpdateAsync(ProductDto product);

        Task DeleteAsync(int id);

        Task<bool> IsExistAsync(int id);
    }
}
