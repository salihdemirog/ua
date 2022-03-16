using UlusalAjans.Domain.Dtos;

namespace UIusalAjans.Domain.Dtos
{
    public class ProductDetailDto : ProductDto
    {
        public CategoryDto Category { get; set; }
    }
}
