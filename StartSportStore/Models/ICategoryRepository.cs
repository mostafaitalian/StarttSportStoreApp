using System.Collections.Generic;
using StartSportStore.Models.pages;
namespace StartSportStore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        PagedList<Category> GetCategories(QueryOption options);
    }
}
