using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DTO;
using RealTimeProject.Models;
using System.Linq.Expressions;

namespace RealTimeProject.DAL.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
      
        public ApplicationUserRepository(ApplicationContext context) : base(context)
        {
           
        }
       
    }
}
