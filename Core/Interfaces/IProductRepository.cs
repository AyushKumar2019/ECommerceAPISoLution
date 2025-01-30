using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<string>> GetAllBrandsAsync();
        Task<IReadOnlyList<string>> GetAllTypesAsync();
        Task<Product?> GetProductsByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(string?brand, string? type, string? sort);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<bool> SaveChangesAsync();
        bool productsExists(int id);
    }
}
