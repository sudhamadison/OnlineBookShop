using RealTimeProject.Models;

namespace RealTimeProject.Services.Interfaces
{
    public interface ICategoryService
    {
      
            Task<bool> CreateCategory(Category category);
            IEnumerable<Category> GetAllCategory();
            Task<Category> GetCategoryById(int id);

            Task<bool> UpdateCategory(Category employee);
            Task<bool> DeleteCategory(int id);
        
    }
}
