using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DTO;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.Services.ServiceImplementation
{
    public class ProductImagesService : IProductImagesService
    {
        public IUnitOfWork _unitOfWork;
        public ProductImagesService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<bool> CreateProductImages(ProductImages productImages)
        {
            if (productImages != null)
            {
                await _unitOfWork.ProductImagesRepository.Add(productImages);
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

        public async Task<bool> DeleteProductImages(int id)
        {
            if (id > 0)
            {
                var empDetails = await _unitOfWork.ProductImagesRepository.GetById(id);
                if (empDetails != null)
                {
                    _unitOfWork.ProductImagesRepository.Delete(empDetails);
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

      

      
        public async Task<bool> UpdateProductImages(ProductImages productImage)
        {
            if (productImage != null)
            {
                var cate = await _unitOfWork.ProductImagesRepository.GetById(productImage.ProductId);
                if (cate != null)
                {
                    //cate.Tittle = product.Tittle;
                    //cate.Description = product.Description;
                    //cate.ISBN = product.ISBN;
                    //cate.Author = product.Author;
                    //cate.Price = product.Price;


                    _unitOfWork.ProductImagesRepository.Update(cate);
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

