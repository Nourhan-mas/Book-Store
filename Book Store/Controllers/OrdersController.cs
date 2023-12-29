using Book_Store.Data.Services.Interfaces;
using Book_Store.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Controllers
{
    [Authorize]
	public class OrdersController : Controller
	{
		private readonly IBooksService _booksService;
		private readonly IShoppingCartService _shoppingCartService;
		private readonly IOrdersService _ordersService;
		 
		public OrdersController(IBooksService booksService, IShoppingCartService shoppingCartService, IOrdersService ordersService)
		{
			_booksService = booksService;
			_shoppingCartService = shoppingCartService;
			_ordersService = ordersService;
		}

		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string userRole = User.FindFirstValue(ClaimTypes.Role);

			var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
			return View(orders);
		}

		public async Task<IActionResult> ShoppingCart()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var shoppingCart = await _shoppingCartService.GetShoppingCartByUserId(userId);

			var response = new ShoppingCartVM()
			{
				ShoppingCart = shoppingCart,
				ShoppingCartTotal = _shoppingCartService.GetShoppingCartTotal(userId),
			};

			return View(response);
		}

		public async Task<IActionResult> AddItemToShoppingCart(int id)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var item = await _booksService.GetBookByIdAsync(id);

			if (item != null)
			{
				_shoppingCartService.AddItemToCart(userId, item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}

		public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var item = await _booksService.GetBookByIdAsync(id);
			if (item != null)
			{
				_shoppingCartService.RemoveItemFromCart(userId, item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}


		public async Task<IActionResult> CompleteOrder()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var items = _shoppingCartService.GetShoppingCartItems(userId);
			string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

			await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
			await _shoppingCartService.ClearShoppingCartAsync(userId);

			return View("OrderCompleted");
		}
	}
}
