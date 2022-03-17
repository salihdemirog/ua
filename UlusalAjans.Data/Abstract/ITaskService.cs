using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data.EntityFramework;

namespace UlusalAjans.Data.Abstract
{
    public interface ITaskService
    {
        Task<List<Product>> GetAllProductAsync();

        Task<List<Category>> GetAllCategoryAsync();

        List<Product> GetAllProduct();

        List<Category> GetAllCategory();
    }

    public class TaskService : ITaskService
    {
        private readonly NorthwindContext _context;

        public TaskService(NorthwindContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategory()
        {
            return _context.Categories.ToList();    
        }

        public Task<List<Category>> GetAllCategoryAsync()
        {
            return _context.Categories.ToListAsync();
        }

        public List<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }

        public Task<List<Product>> GetAllProductAsync()
        {
            return _context.Products.ToListAsync();
        }
    }
}
