using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DTO;
using RealTimeProject.Models;
using System.Linq.Expressions;

namespace RealTimeProject.DAL.Repositories
{
    public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
    {
      
        public OrderHeaderRepository(ApplicationContext context) : base(context)
        {
           
        }
       
    }
}
