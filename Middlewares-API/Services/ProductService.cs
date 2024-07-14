using Middlewares_API.Models.Entity;

namespace Middlewares_API.Services;

public static class ProductService
{ 
    private readonly static List<Product> _products =  new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 1000,Quantity = 0,  CategoryId = 1, Category = CategoryService.GetCategoryById(1) },
        new Product { Id = 2, Name = "Book", Price = 20, Quantity = 0, CategoryId = 2, Category  = CategoryService.GetCategoryById(2) }
    };
    
    public static  List<Product> GetProducts()
    {
        return _products;
    }
    
    public static decimal Calculate()
    {
        var sumPrice = 0.0M;
        var totalQuantity = 0;
        foreach (var product in _products)
        {
            sumPrice += product.Price;
            totalQuantity += product.Quantity;
        }

        var unitPrice = sumPrice / totalQuantity; 
        return unitPrice;
    }
}