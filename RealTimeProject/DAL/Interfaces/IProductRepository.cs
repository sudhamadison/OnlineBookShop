using RealTimeProject.DTO;
using RealTimeProject.Models;

namespace RealTimeProject.DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public IEnumerable<ProductViewModel> GetAllProductsWithCategory();
        public  Task<Product> GetProductDetailsWithProductImages(int productId);
       
    }
}
