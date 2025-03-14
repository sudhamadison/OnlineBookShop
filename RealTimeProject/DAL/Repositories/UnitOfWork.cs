using RealTimeProject.DAL.Interfaces;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
       
            private readonly ApplicationContext _context;
            public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
       
        public IProductImagesRepository ProductImagesRepository { get; }
        public IShoppingCartRepository ShoppingCartRepository { get; }
        public IApplicationUserRepository ApplicationUserRepository { get; }
        public IOrderHeaderRepository OrderHeaderRepository { get; }

        public UnitOfWork(ApplicationContext context, ICategoryRepository categoryRepository, IProductRepository productRepository, IProductImagesRepository productImagesRepository, IShoppingCartRepository shoppingCartRepository, IApplicationUserRepository  applicationUserRepository)
            {
                _context = context;
                CategoryRepository = categoryRepository;
                ProductRepository = productRepository;
            ProductImagesRepository = productImagesRepository;
            ShoppingCartRepository = shoppingCartRepository;
            ApplicationUserRepository = applicationUserRepository;


        }

        public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {

                if (disposing)
                {
                    _context.Dispose();
                }
            }

            public int Save()
            {
                return _context.SaveChanges();
            }

      
    }
    }

