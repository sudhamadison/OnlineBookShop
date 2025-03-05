namespace RealTimeProject.Models.ViewModel
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> shoppingCartList { get; set; }
        public OrderHeader orderHeader { get; set; }
    }
}
