using Microsoft.EntityFrameworkCore;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DTO;
using RealTimeProject.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RealTimeProject.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _context;
        public ProductRepository(ApplicationContext context) : base(context)
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

                                    //    public IEnumerable<ProductViewModel> GetAllProductwithCategoryAndImages()
                                    //    {
                                    //        var result = _context.Product
                                    //.Join(_context.Category,
                                    //      product => product.CategoryId,
                                    //      category => category.CategoryId,
                                    //      (product, category) => new { product, category })
                                    //.Join(_context.ProductImages,
                                    //      combined => combined.product.ProductId,
                                    //      productImage => productImage.ProductId,
                                    //      (combined, productImage) => new { combined.product, combined.category, productImage })
                                    //.Select(result => new
                                    //{

                                    //    ProductId = result.product.ProductId,
                                    //    Tittle = result.product.Tittle,
                                    //    ISBN = result.product.ISBN,
                                    //    Description = result.product.Description,
                                    //    Author = result.product.Author,
                                    //    Price = result.product.Price,
                                    //    CategoryName = result.category.Name,
                                    //    ImageUrl = result.productImage.ImageUrl
                                    //});

                                    //     return result.ToList();
                                    //    }
        public async Task<Product> GetProductDetailsWithProductImages(int productId)
        {
            var query = await _context.Product.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.ProductId == productId);
            return query;
        }
    }
}
