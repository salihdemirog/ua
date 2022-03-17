using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Dtos;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data.Abstract;

namespace UlusalAjans.Data.EntityFramework
{
    public class EfProductService : IProductService
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public EfProductService(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            _context.Products.Remove(new Product
            {
                Id = id
            });

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryIdAsync(int categoryId)
        {
            var products =await _context.Products
                .Where(t => t.CategoryId == categoryId).ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDetailDto> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(x => x.Category)
                .SingleOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<ProductDetailDto>(product);
        }

        public async Task<ProductDto> InsertAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productDto.Id = product.Id;
            return productDto;
        }

        public Task<bool> IsExistAsync(int id)
        {
            return _context.Products.AsNoTracking().AnyAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            //var addedProduct = GetById(product.Id);
            //addedProduct.Name = product.Name;
            //addedProduct.UnitPrice = product.UnitPrice;

            var product = _mapper.Map<Product>(productDto);

            var entry = _context.Entry(product);
            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
