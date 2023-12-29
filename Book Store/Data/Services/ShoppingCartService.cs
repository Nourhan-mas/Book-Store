using Book_Store.Data.Services.Interfaces;
using Book_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Data.Services
{
    public class ShoppingCartService : IShoppingCartService
	{
		public AppDbContext _context { get; set; }

		public ShoppingCartService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<ShoppingCart> GetShoppingCartByUserId(string userId)
		{
			var shoppingCart = await _context.ShoppingCart
				.Include(n => n.ShoppingCartItems)
				.ThenInclude(n => n.Book)
				.Include(n => n.User)
				.FirstOrDefaultAsync(n => n.UserId == userId);
			if (shoppingCart == null)
			{
				// If the shopping cart doesn't exist, create a new one
				shoppingCart = new ShoppingCart
				{
					UserId = userId,
					ShoppingCartItems = new List<ShoppingCartItem>()
				};

				_context.ShoppingCart.Add(shoppingCart);
				await _context.SaveChangesAsync();
			}

			return shoppingCart;
		}

		public void AddItemToCart(string userId, Books book)
		{
			var shoppingCart = GetShoppingCartByUserId(userId).Result;
			var shoppingCartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(n => n.Book.Id == book.Id);
			if (shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem()
				{
					ShoppingCartId = shoppingCart.Id,
					Book = book,
					Amount = 1
				};

				_context.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				shoppingCartItem.Amount++;
			}
			_context.SaveChanges();
		}

		public void RemoveItemFromCart(string userId, Books book)
		{
			var shoppingCart = GetShoppingCartByUserId(userId).Result;
			var shoppingCartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(n => n.Book.Id == book.Id);
			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}
			_context.SaveChanges();
		}

		public List<ShoppingCartItem> GetShoppingCartItems(string userId)
		{
			var shoppingCart = GetShoppingCartByUserId(userId).Result;
			return shoppingCart?.ShoppingCartItems.ToList() ?? new List<ShoppingCartItem>();
		}

		public double GetShoppingCartTotal(string userId)
		{
			var shoppingCart = GetShoppingCartByUserId(userId).Result;  // Synchronously get the shopping cart for simplicity
			return shoppingCart?.ShoppingCartItems.Sum(n => n.Book.Price * n.Amount) ?? 0;
		}
		public async Task ClearShoppingCartAsync(string userId)
		{
			var shoppingCart = await GetShoppingCartByUserId(userId);
			var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == shoppingCart.Id).ToListAsync();
			_context.ShoppingCartItems.RemoveRange(items);
			await _context.SaveChangesAsync();
		}
	}
}
