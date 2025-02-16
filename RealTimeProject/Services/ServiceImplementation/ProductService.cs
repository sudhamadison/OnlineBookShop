using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DTO;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.Services.ServiceImplementation
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<bool> CreateProduct(Product product)
        {
            if (product != null)
            {
                await _unitOfWork.ProductRepository.Add(product);
                var result = _unitOfWork.Save();
                if (result > 0)
                {
                    return true;
                }
                else
                { return false; }
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            if (id > 0)
            {
                var empDetails = await _unitOfWork.ProductRepository.GetById(id);
                if (empDetails != null)
                {
                    _unitOfWork.ProductRepository.Delete(empDetails);
                    var result = _unitOfWork.Save();
                    if (result > 0)
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
            return false;
        }

        public IEnumerable<ProductViewModel> GetAllProduct()
        {
            var empDetails =  _unitOfWork.ProductRepository.GetAllProductsWithCategory();
            return empDetails;

        }

        public async Task<Product> GetProductById(int id)
        {
            if (id > 0)
            {
                var empDetails = await _unitOfWork.ProductRepository.GetProductDetailsWithProductImages(id);
                if (empDetails != null)
                {
                    return empDetails;
                }

            }
            return null;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            if (product != null)
            {
                var cate = await _unitOfWork.ProductRepository.GetById(product.ProductId);
                if (cate != null)
                {
                    cate.Tittle = product.Tittle;
                    cate.Description = product.Description;
                    cate.ISBN = product.ISBN;
                    cate.Author = product.Author;
                    cate.Price = product.Price;


                    _unitOfWork.ProductRepository.Update(cate);
                    var result = _unitOfWork.Save();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;

            }
            return false;
        }
    }
}

