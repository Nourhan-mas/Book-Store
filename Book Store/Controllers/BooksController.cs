using Book_Store.Data.Services.Interfaces;
using Book_Store.Data.Static;
using Book_Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Store.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]

    public class BooksController : Controller
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAllAsync(n => n.Shop);
            return View(allBooks);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = await _service.GetAllAsync(n => n.Shop);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allBooks.Where(n => n.BookName.ToLower().Contains(searchString.ToLower()) || n.Overview.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allBooks.Where(n => string.Equals(n.BookName, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Overview, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allBooks);
        }
        // GET: Books/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");
            return View(bookDetails);
        }


        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var bookDropdownsData = await _service.GetNewBookDropdownsValues();

            ViewBag.Shops = new SelectList(bookDropdownsData.Shops, "Id", "ShopName");
            ViewBag.Publishers = new SelectList(bookDropdownsData.Publishers, "Id", "FullName");
            ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "Name");

            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewBookVM book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropdownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.Shops = new SelectList(bookDropdownsData.Shops, "Id", "ShopName");
                ViewBag.Publishers = new SelectList(bookDropdownsData.Publishers, "Id", "FullName");
                ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "Name");

                return View(book);
            }

            await _service.AddNewBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var response = new NewBookVM()
            {
                Id = bookDetails.Id,
                Name = bookDetails.BookName,
                Overview = bookDetails.Overview,
                Price = bookDetails.Price,
                ImageURL = bookDetails.ImageURL,
                Bookcategory = bookDetails.Category,
                ShopId = bookDetails.ShopId,
                PublisherId = bookDetails.PublisherId,
                AuthorId = bookDetails.AuthorId,
            };

            var bookDropdownsData = await _service.GetNewBookDropdownsValues();
            ViewBag.Shops = new SelectList(bookDropdownsData.Shops, "Id", "ShopName");
            ViewBag.Publishers = new SelectList(bookDropdownsData.Publishers, "Id", "FullName");
            ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "Name");

            return View(response);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewBookVM book)
        {
            if (id != book.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var bookDropdownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.Shops = new SelectList(bookDropdownsData.Shops, "Id", "ShopName");
                ViewBag.Publishers = new SelectList(bookDropdownsData.Publishers, "Id", "FullName");
                ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "Name");

                return View(book);
            }

            await _service.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

    }
}
