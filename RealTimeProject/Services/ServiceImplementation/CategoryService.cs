using RealTimeProject.DAL.Interfaces;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.Services.ServiceImplementation
{
    public class CategoryService : ICategoryService
    {
        public IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<bool> CreateCategory(Category category)
        {
            if (category != null)
            {
                await _unitOfWork.CategoryRepository.Add(category);
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

        public async Task<bool> DeleteCategory(int id)
        {
            if (id > 0)
            {
                var empDetails = await _unitOfWork.CategoryRepository.GetById(id);
                if (empDetails != null)
                {
                    _unitOfWork.CategoryRepository.Delete(empDetails);
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

        public  IEnumerable<Category> GetAllCategory()
        {
            var empDetails =  _unitOfWork.CategoryRepository.GetAll();
            return empDetails;

        }

        public async Task<Category> GetCategoryById(int id)
        {
            if (id > 0)
            {
                var empDetails = await _unitOfWork.CategoryRepository.GetById(id);
                if (empDetails != null)
                {
                    return empDetails;
                }

            }
            return null;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            if (category != null)
            {
                var cate = await _unitOfWork.CategoryRepository.GetById(category.CategoryId);
                if (cate != null)
                {
                    cate.Name = category.Name;
                    cate.Description = category.Description;

                    _unitOfWork.CategoryRepository.Update(cate);
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

