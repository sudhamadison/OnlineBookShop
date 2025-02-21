using Microsoft.EntityFrameworkCore;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.Services.ServiceImplementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        public IUnitOfWork _unitOfWork;
        public ShoppingCartService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Update(ShoppingCart shoppingCart)
        {
            _unitOfWork.ShoppingCartRepository.Update(shoppingCart);
        }
    }
}
