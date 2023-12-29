using Book_Store.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
	{
		private readonly IShoppingCartService _shoppingCartService;
		public ShoppingCartSummary(IShoppingCartService shoppingCartService)
		{
			_shoppingCartService = shoppingCartService;
		}

		public IViewComponentResult Invoke()
		{
			var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
			var items = _shoppingCartService.GetShoppingCartItems(userId);

			return View(items.Count);
		}
	}
}
