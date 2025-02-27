using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.Migrations;
using RealTimeProject.Models;
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
            var claimsIdentity=(ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<ShoppingCart> sk = _unitOfWork.ShoppingCartRepository.GetList(u => u.ApplicationUserId == userId, includeProperties: "Product").ToList();
            //check Get() and GetList() in Generic Repository(Instead of joining more tables[Product,Category,ProductImages] we have created this Get[This one with filter to bring particular record] and GetList[This one without filter to bring list of record] method using  where clause[Filter].) with include properties.

            IEnumerable<ProductImages> productImages = _unitOfWork.ProductImagesRepository.GetList(); //this line will bring all the imges from the productImages table.         
            foreach(var cart in sk)
            {
                cart.Product.ProductImages = productImages.Where(x => x.ProductId == cart.Product.ProductId).ToList();
                cart.Price = GetPriceBasedOnQuantity(cart);
            }
            return View();
        }

        private decimal GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            decimal finalPrice = 0;
            if(shoppingCart==null)
            {
                finalPrice = shoppingCart.Count * shoppingCart.Price;
            }
            return finalPrice;
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb=_unitOfWork.ShoppingCartRepository.Get(x=>x.ShoppingCartId== cartId);
            cartFromDb.Count = cartFromDb.Count  + 1;
            _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction ("Index");
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
    }
}
