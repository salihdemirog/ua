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

        public void Delete(int id)
        {
            _context.Products.Remove(new Product
            {
                Id = id
            });

            _context.SaveChanges();
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var products = _context.Products;

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public ProductDetailDto GetById(int id)
        {
            var product = _context.Products
                .Include(x => x.Category)
                .SingleOrDefault(p => p.Id == id);

            return _mapper.Map<ProductDetailDto>(product);
        }

        public ProductDto Insert(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _context.Products.Add(product);
            _context.SaveChanges();

            productDto.Id = product.Id;
            return productDto;
        }

        public bool IsExist(int id)
        {
            return _context.Products.AsNoTracking().Any(p => p.Id == id);
        }

        public void Update(ProductDto productDto)
        {
            //var addedProduct = GetById(product.Id);
            //addedProduct.Name = product.Name;
            //addedProduct.UnitPrice = product.UnitPrice;

            var product = _mapper.Map<Product>(productDto);

            var entry = _context.Entry(product);
            entry.State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
