﻿using RealTimeProject.Models;
using RealTimeProject.Services.Interfaces;

namespace RealTimeProject.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
      
        IProductImagesRepository ProductImagesRepository { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }




        int Save();
        
    }
}
