using Microsoft.EntityFrameworkCore;
using RealTimeProject.DAL.Interfaces;
using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.Services.ServiceImplementation
{
    public class ApplicationUserService : IApplicationUserService
    {
        public IUnitOfWork _unitOfWork;
        public ApplicationUserService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public void Update(ApplicationUser applicationUser)
        {
            _unitOfWork.ApplicationUserRepository.Update(applicationUser);
        }
    }
}
