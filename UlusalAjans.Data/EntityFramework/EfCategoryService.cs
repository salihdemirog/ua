using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UIusalAjans.Domain.Entities;
using UlusalAjans.Data.Abstract;
using UlusalAjans.Domain.Dtos;

namespace UlusalAjans.Data.EntityFramework
{
    public class EfCategoryService : ICategoryService
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public EfCategoryService(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public IEnumerable<CategoryDto> GetAll()
        {
            var categories = _context.Categories;

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public CategoryDto GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(p => p.Id == id);
            return _mapper.Map<CategoryDto>(category);
        }

        public CategoryDto Insert(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            _context.Categories.Add(category);
            _context.SaveChanges();

            categoryDto.Id = category.Id;
            return categoryDto;
        }

        public bool IsExist(int id)
        {
            return _context.Categories.AsNoTracking().Any(p => p.Id == id);
        }

        public void Update(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            var entry = _context.Entry(category);
            entry.State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
