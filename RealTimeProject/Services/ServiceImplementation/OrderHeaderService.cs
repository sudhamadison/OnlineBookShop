using Microsoft.EntityFrameworkCore;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.Services.ServiceImplementation
{
    public class OrderHeaderService : IOrderHeaderService
    {
        public IUnitOfWork _unitOfWork;
        public OrderHeaderService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Update(OrderHeader orderHeader)
        {
            _unitOfWork.OrderHeaderRepository.Update(orderHeader);
        }
    }
}
