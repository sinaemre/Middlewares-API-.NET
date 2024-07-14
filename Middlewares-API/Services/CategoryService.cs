using Middlewares_API.Models.Entity;

namespace Middlewares_API.Services;

public static class CategoryService
{
    private static readonly List<Category> _categories = new List<Category>
    {
        new Category { Id = 1, Name = "Electronics" },
        new Category { Id = 2, Name = "Books" }
    };
    
    public static List<Category> GetCategories()
    {

        return _categories;
    }

    public static Category GetCategoryById(int id)
    {
        var category = _categories.FirstOrDefault(x => x.Id == id);

        return category;
    }
}