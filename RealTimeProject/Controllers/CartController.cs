using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.DAL.Repositories;
using RealTimeProject.Migrations;
using RealTimeProject.Models;
using RealTimeProject.Models.ViewModel;
using System.Security.Claims;

namespace RealTimeProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CartController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //List<ShoppingCart> sk = _unitOfWork.ShoppingCartRepository.GetList(u => u.ApplicationUserId == userId, includeProperties: "Product").ToList();

            //check Get() and GetList() in Generic Repository(Instead of joining more tables[Product,Category,ProductImages] we have created this Get[This one with filter to bring particular record] and GetList[This one without filter to bring list of record] method using  where clause[Filter].) with include properties.
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = _unitOfWork.ShoppingCartRepository.GetList(u => u.ApplicationUserId == userId, includeProperties: "Product").ToList(),

                OrderHeader = new()
            };
            IEnumerable<ProductImages> productImages = _unitOfWork.ProductImagesRepository.GetList(); //this line will bring all the imges from the productImages table.         
            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                cart.Product.ProductImages = productImages.Where(x => x.ProductId == cart.Product.ProductId).ToList();//This line helps to assign particular product images into ProductImages column in ShoppingCart Table.
                cart.Price = GetPriceBasedOnQuantity(cart);
                shoppingCartVM.OrderHeader.OrderTotal += cart.Price;
            }
            return View(shoppingCartVM);
        }
      
        private decimal GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            decimal finalPrice = 0;
            if (shoppingCart != null)
            {
                finalPrice = shoppingCart.Count * shoppingCart.Price;
            }
            return finalPrice;
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCartRepository.Get(x => x.ShoppingCartId == cartId);
            cartFromDb.Count = cartFromDb.Count + 1;
            _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCartRepository.Get(x => x.ShoppingCartId == cartId);
            if (cartFromDb.Count != null && cartFromDb.Count <= 1)
            {
                _unitOfWork.ShoppingCartRepository.Delete(cartFromDb);

            }
            else
            {
                cartFromDb.Count = cartFromDb.Count - 1;

            }
            _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //List<ShoppingCart> sk = _unitOfWork.ShoppingCartRepository.GetList(u => u.ApplicationUserId == userId, includeProperties: "Product").ToList();

            //check Get() and GetList() in Generic Repository(Instead of joining more tables[Product,Category,ProductImages] we have created this Get[This one with filter to bring particular record] and GetList[This one without filter to bring list of record] method using  where clause[Filter].) with include properties.
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = _unitOfWork.ShoppingCartRepository.GetList(u => u.ApplicationUserId == userId, includeProperties: "Product").ToList(),
                OrderHeader = new()
            };

            shoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == userId);
            shoppingCartVM.OrderHeader.Name = shoppingCartVM.OrderHeader.ApplicationUser.Name;
            shoppingCartVM.OrderHeader.Address = shoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            shoppingCartVM.OrderHeader.PhoneNumber = shoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            shoppingCartVM.OrderHeader.City = shoppingCartVM.OrderHeader.ApplicationUser.City;
            shoppingCartVM.OrderHeader.Carrier = "Indigo";

            IEnumerable<ProductImages> productImages = _unitOfWork.ProductImagesRepository.GetList(); //this line will bring all the imges from the productImages table.         
            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                cart.Product.ProductImages = productImages.Where(x => x.ProductId == cart.Product.ProductId).ToList();//This line helps to assign particular product images into ProductImages column in ShoppingCart Table.
                cart.Price = GetPriceBasedOnQuantity(cart);
                shoppingCartVM.OrderHeader.OrderTotal += cart.Price;
			}
            return View(shoppingCartVM);
        }

    }
}

