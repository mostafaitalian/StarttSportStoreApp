using StartSportStore.Models.pages;
using System.Collections.Generic;
using System.Linq;

namespace StartSportStore.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext con)
        {
            context = con;
        }
        public IEnumerable<Category> Categories => context.Categories.ToArray();

        //IEnumerable<Category> ICategoryRepository.Categories { get => context.Categories; }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public PagedList<Category> GetCategories(QueryOption options)
        {
            return new PagedList<Category>(context.Categories, options);
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }
    }
}
