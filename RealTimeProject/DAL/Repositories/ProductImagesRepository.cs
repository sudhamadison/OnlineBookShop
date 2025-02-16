using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DTO;
using RealTimeProject.Models;

namespace RealTimeProject.DAL.Repositories
{
    public class ProductImagesRepository : GenericRepository<ProductImages>, IProductImagesRepository
    {
        private readonly ApplicationContext _context;
        public ProductImagesRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<ProductViewModel> GetAllProductsWithCategory()
        {
            var query = _context.Product
                  .Join(
                      _context.Category,
                      p => p.CategoryId,
                      c => c.CategoryId,
                      (p, c) => new ProductViewModel
                      {
                          ProductId = p.ProductId,
                          Tittle = p.Tittle,
                          Description = p.Description,
                          ISBN = p.ISBN,
                          Author = p.Author,
                          Price = p.Price,
                          CategoryName = c.Name
                      });
            return query.ToList();
        }

    }
}
