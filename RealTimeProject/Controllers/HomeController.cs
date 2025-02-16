using Microsoft.AspNetCore.Mvc;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DAL.Repositories;
using RealTimeProject.DTO;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;
using RealTimeProject.Services.ServiceImplementation;
using System.Diagnostics;

namespace RealTimeProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _logger = logger;
            _productService = productService;
           _unitOfWork = unitOfWork ;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.ProductRepository.GetList(includeProperties: "Category,ProductImages");
            return View(productList);


        }
        [HttpGet]
        public IActionResult Details(int productId)
        {
            ShoppingCart shoppingcart = new ShoppingCart()
            {
                Product = _unitOfWork.ProductRepository.Get(u => u.ProductId == productId, includeProperties: "Category,ProductImages"),
                Count = 1,
                ProductId = productId
            };
            return View(shoppingcart);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
