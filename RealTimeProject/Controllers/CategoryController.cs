using Microsoft.AspNetCore.Mvc;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        public CategoryController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var ListOfCategory = _CategoryService.GetAllCategory();
            return View(ListOfCategory);
        }
        [HttpGet]
        public async Task<JsonResult> GetCategoriesUsingSearch()
        {
            JqueryDataTableParam param = new JqueryDataTableParam();
            param.Search = "kId";
            param.SortColumn = "Description";
            param.SortOrder = "asc"; 
            var categories=  _CategoryService.GetAllCategory();
            if (!string.IsNullOrEmpty(param.Search))
            {
                var ListOfCategory= categories.Where(x=>x.Name.ToLower().Contains(param.Search.ToLower()) ||
                x.Description.ToLower().Contains(param.Search.ToLower())).ToList();
            }
            if(!string.IsNullOrEmpty(param.SortColumn))
            {
               if(param.SortColumn=="Name")

                {
                    var SortedList = param.SortOrder == "asc" ? categories.OrderBy(x => x.Name).ToList() : categories.OrderByDescending(x => x.Name).ToList();
                }
                else if (param.SortColumn == "Description")
                {
                    var SortedList = param.SortOrder == "asc" ? categories.OrderBy(x => x.Description).ToList() : categories.OrderByDescending(x => x.Description).ToList();
                }
            }

            return null;

        }
        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category e)
        {
            try
            {
                var emp = await _CategoryService.CreateCategory(e);
                TempData["Success"] = " Category name added Successfully";
                return RedirectToAction("GetAllCategory");
            }
            catch (Exception ex) 
            {
                TempData["error"] = "This Category name is already Exsisted";
                return RedirectToAction("GetAllCategory");

            }
           
        }
        [HttpGet]

        public async Task<ActionResult> EditCategory(int id)
        {
            Category e = await _CategoryService.GetCategoryById(id);
            return View(e);
        }
        [HttpPost]

        public async Task<ActionResult> EditCategory(Category emp)
        {
            if (emp != null)
            {
                var empIsUpdated = await _CategoryService.UpdateCategory(emp);
                if (empIsUpdated)
                {
                    return RedirectToAction("GetAllCategory");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Category e = await _CategoryService.GetCategoryById(id);
            if (e != null)
            {
                return View(e);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id > 0)
            {
                var empIsdeleted = await _CategoryService.DeleteCategory(id);
                if (empIsdeleted)
                {
                    return RedirectToAction("GetAllCategory");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }

    }
}

