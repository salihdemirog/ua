using Microsoft.EntityFrameworkCore;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data.Abstract;

namespace UlusalAjans.Data.EntityFramework
{
    public class EfCategoryService : ICategoryService
    {
        private readonly NorthwindContext _context;   
        public EfCategoryService(NorthwindContext context)
        {
            _context = context; 

        }

        public void Delete(int id)
        {

            _context.Categories.Remove(new Category
            {
                Id = id
            });
            //_context.Products.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetById(int id)
        {
            return _context.Categories.SingleOrDefault(p => p.Id == id);
            
        }

        public Category Insert(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

        public bool IsExist(int id)
        {
            return _context.Categories.AsNoTracking().Any(p => p.Id == id);   
        }

        public void Update(Category category)
        {
            var entry = _context.Entry(category);
            entry.State = EntityState.Modified;
            _context.SaveChanges();

            
        }

     
    }
}
