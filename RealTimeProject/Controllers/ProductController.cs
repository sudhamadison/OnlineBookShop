using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DAL.Repositories;
using RealTimeProject.DTO;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;
        private readonly ICategoryService _CategoryService;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IProductImagesService _ProductImagesService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ProductController(IProductService ProductService,ICategoryService CategoryService, IUnitOfWork UnitOfWork, IProductImagesService ProductImagesService, IWebHostEnvironment WebHostEnvironment)
        {
            _ProductService = ProductService;
            _CategoryService = CategoryService;
            _UnitOfWork = UnitOfWork;
            _ProductImagesService = ProductImagesService;
            _WebHostEnvironment = WebHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var ListOfProduct =  _ProductService.GetAllProduct();
            return View(ListOfProduct);
        }
        public ActionResult Index() //search mechanism with pagination in product table.
                                    //But here we r using datatable library for search.
                                    //In thid method we r giving search data in the method itself.
                                    //If u want to try this give this method name in layout.cshtml->product controller->asp-action
                                    //searching is on isbn,author,tittle,description not on category.
        {
            JqueryDataTableParam param = new JqueryDataTableParam();
            param.Search = "cOoK";
            param.DiaplayStart = 1;
            param.DisplayLength = 100;

            List<Product> ProductList = _UnitOfWork.ProductRepository.GetList().ToList();
            if (!string.IsNullOrEmpty(param.Search))
            {
                ProductList = ProductList.Where(x => x.ISBN.ToLower().Contains(param.Search.ToLower())
                || x.Author.ToLower().Contains(param.Search.ToLower())
                || x.Tittle.ToLower().Contains(param.Search.ToLower())
                || x.Description.ToLower().Contains(param.Search.ToLower())).ToList();
            }

            ProductList = ProductList.Skip(param.DiaplayStart)
                .Take(param.DisplayLength).ToList();
            var totalRecords = ProductList.Count();
            return View(ProductList);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {

            IEnumerable<SelectListItem> categorynames = _CategoryService.GetAllCategory().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.CategoryId.ToString(),
            }).ToList();
            ViewData["CategoryName"] = categorynames;
            return View();
            
        }
        [HttpPost]


        public async Task<IActionResult> AddProduct(Product e, IEnumerable<IFormFile?> Multiplefiles)
        {
            try

            {
                List<ProductImages> images = new List<ProductImages>();

                ModelState.Remove("Category");
                //if (e.ImageUrl == null)
                //{
                //    ModelState.AddModelError("ImageUrl", "ImageUrl is required");
                //    return View(e);
                //}


                if (ModelState.IsValid)
                {
                    string wwwRootPath = _WebHostEnvironment.WebRootPath;
                    if (Multiplefiles != null)
                    {
                        foreach (var file in Multiplefiles)

                        {
                            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            string ProductFolderPath = Path.Combine(wwwRootPath, @"images\Product");
                            using (var fileStream = new FileStream(Path.Combine(ProductFolderPath, filename), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                            //e.ImageUrl = @"images\Product\" + filename;
                            var pimage = new ProductImages();
                            pimage.ImageUrl = @"images\Product\" + filename;
                            pimage.ImageType = "Test";
                            images.Add(pimage);
                        }

                        e.ProductImages = images;
                        e.ImageUrl = "test";




                    }

                    var emp = await _ProductService.CreateProduct(e);
                    TempData["Success"] = " Product name added Successfully";
                }

                return RedirectToAction("GetAllProduct");
            }
            catch (Exception ex)
            {
                TempData["error"] = "This Product name is already Exsisted";
                return RedirectToAction("GetAllProduct");

            }

        }




        [HttpGet]

        public async Task<ActionResult> EditProduct(int id)
        {
            Product e = await _ProductService.GetProductById(id);
            return View(e);
        }
        [HttpPost]

        public async Task<ActionResult> EditProduct(Product emp)
        {
            if (emp != null)
            {
                var empIsUpdated = await _ProductService.UpdateProduct(emp);
                if (empIsUpdated)
                {
                    return RedirectToAction("GetAllProduct");
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
            Product e = await _ProductService.GetProductById(id);
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
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id > 0)
            {
                var empIsdeleted = await _ProductService.DeleteProduct(id);
                if (empIsdeleted)
                {
                    return RedirectToAction("GetAllProduct");
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }
        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = _UnitOfWork.ProductImagesRepository.Get(u=>u.ProductId == imageId);
            if (imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, imageToBeDeleted.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _UnitOfWork.ProductImagesRepository.Delete(imageToBeDeleted);
                _UnitOfWork.Save();
            }
           return RedirectToAction("GetAllProduct");

        }

    }
}

