using RealTimeProject.DAL.Interfaces;
using RealTimeProject.Models;

namespace RealTimeProject.DAL.Repositories
{
    public class  ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationContext _context;
        public ShoppingCartRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
       
    } }
