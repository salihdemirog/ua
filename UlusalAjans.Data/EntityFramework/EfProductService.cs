using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data.Abstract;

namespace UlusalAjans.Data.EntityFramework
{
    public class EfProductService : IProductService
    {
        private readonly NorthwindContext _context;

        public EfProductService(NorthwindContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Products.Remove(new Product
            {
                Id = id
            });

            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product GetById(int id)
        {
            return _context.Products.SingleOrDefault(p => p.Id == id);
        }

        public Product Insert(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

        public bool IsExist(int id)
        {
            return _context.Products.AsNoTracking().Any(p => p.Id == id);
        }

        public void Update(Product product)
        {
            //var addedProduct = GetById(product.Id);
            //addedProduct.Name = product.Name;
            //addedProduct.UnitPrice = product.UnitPrice;

            var entry = _context.Entry(product);
            entry.State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
