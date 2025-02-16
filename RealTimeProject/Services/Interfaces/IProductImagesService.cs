using RealTimeProject.DTO;
using RealTimeProject.Models;

namespace RealTimeProject.Services.Interfaces
{
    public interface IProductImagesService
    {
      
            Task<bool> CreateProductImages(ProductImages productImages);
          

            Task<bool> UpdateProductImages(ProductImages productImages);
            Task<bool> DeleteProductImages(int ProductImageId);
        
    }
}
