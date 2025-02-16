using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DTO;
using RealTimeProject.Models;

namespace RealTimeProject.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
      
        public CategoryRepository(ApplicationContext context) : base(context)
        {
           
        }
        

    }
}
