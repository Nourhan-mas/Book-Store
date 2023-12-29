using Book_Store.Models;

namespace Book_Store.Data.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetShoppingCartByUserId(string userId);
        void AddItemToCart(string userId, Books book);
        void RemoveItemFromCart(string userId, Books book);
        List<ShoppingCartItem> GetShoppingCartItems(string userId);
        double GetShoppingCartTotal(string userId);
        Task ClearShoppingCartAsync(string userId);
    }
}
