using RealTimeProject.DTO;
using RealTimeProject.Models;

namespace RealTimeProject.Services.Interfaces
{
    public interface IProductService
    {
      
            Task<bool> CreateProduct(Product Product);
            IEnumerable<ProductViewModel> GetAllProduct();
            Task<Product> GetProductById(int id);

            Task<bool> UpdateProduct(Product employee);
            Task<bool> DeleteProduct(int id);
        
    }
}
