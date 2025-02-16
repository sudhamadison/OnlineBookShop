namespace RealTimeProject.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
      
        IProductImagesRepository ProductImagesRepository { get; }
        int Save();
    }
}
