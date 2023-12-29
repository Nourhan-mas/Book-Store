using Book_Store.Data.Services.Interfaces;
using Book_Store.Data.Static;
using Book_Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ShopsController : Controller
    {
        private readonly IShopsService _service;

        public ShopsController(IShopsService service)
        {
            _service = service;
        } 

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allShops = await _service.GetAllAsync();
            return View(allShops);
        }


        //Get: Shops/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ShopLogo,ShopName,Description")] Shop shop)
        {
            if (!ModelState.IsValid) return View(shop);
            await _service.AddAsync(shop);
            return RedirectToAction(nameof(Index));
        }

        //Get: Shops/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var shopDetails = await _service.GetByIdAsync(id);
            if (shopDetails == null) return View("NotFound");
            return View(shopDetails);
        }

        //Get: Shops/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var shopDetails = await _service.GetByIdAsync(id);
            if (shopDetails == null) return View("NotFound");
            return View(shopDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShopLogo,ShopName,Description")] Shop shop)
        {
            if (!ModelState.IsValid) return View(shop);
            await _service.UpdateAsync(id, shop);
            return RedirectToAction(nameof(Index));
        }

        //Get: Shops/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var shopDetails = await _service.GetByIdAsync(id);
            if (shopDetails == null) return View("NotFound");
            return View(shopDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var shopDetails = await _service.GetByIdAsync(id);
            if (shopDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
