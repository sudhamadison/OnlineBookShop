using Microsoft.AspNetCore.Mvc;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DAL.Repositories;
using RealTimeProject.DTO;
using RealTimeProject.Migrations;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;
using RealTimeProject.Services.ServiceImplementation;
using System.Diagnostics;
using System.Security.Claims;

namespace RealTimeProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IUnitOfWork unitOfWork, IProductRepository productRepository, IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _productService = productService;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _shoppingCartService = shoppingCartService;
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

        [HttpPost]
        public IActionResult Details(ShoppingCart shoppingCart) //method for AddToCart
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; //once user logged in only we can add items in cart.That is our design.with the help of that we r getting user identity and assign that into a variable called claimsIdentity.
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value; // we r getting the value(Id in the AspNetUser Table) of claimIdentity(Current user)[current user Id number].
            var productFromDb = _unitOfWork.ProductRepository.Get(u => u.ProductId == shoppingCart.ProductId, includeProperties: "Category,ProductImages");

            var cartFromDb=_unitOfWork.ShoppingCartRepository.Get(u=>u.ApplicationUserId == userId &&  u.ProductId == shoppingCart.ProductId);//cart from db is the already added product in the cart.
            shoppingCart.ApplicationUserId = userId;
            shoppingCart.Price = productFromDb.Price;
            if(cartFromDb != null) //if we add same product again(more)  that should update in the database.It should not create new entry.
            {
                //shoppingCart.ShoppingCartId = cartFromDb.ShoppingCartId;   //for update statement we need to know which(where condition)  row should be updated..    
                //shoppingCart.Count += cartFromDb.Count;
                //_unitOfWork.ShoppingCartRepository.Update(shoppingCart);
                //_unitOfWork.Save();

                cartFromDb.Count += shoppingCart.Count ;
                _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCartRepository.Add(shoppingCart);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
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
