using Book_Store.Data.Base;
using Book_Store.Data.Services.Interfaces;
using Book_Store.Data.ViewModels;
using Book_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Data.Services
{
    public class BooksService : EntityBaseRepository<Books>, IBooksService
    {
        private readonly AppDbContext _context;
        public BooksService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewBookAsync(NewBookVM data)
        {
            var newBook = new Books()
            {
                BookName = data.Name,
                Overview = data.Overview,
                Price = data.Price,
                ImageURL = data.ImageURL,
                ShopId = data.ShopId,
                Category = data.Bookcategory,
                PublisherId = data.PublisherId ,
                AuthorId = data.AuthorId
        };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            
            
        }

        public async Task<Books> GetBookByIdAsync(int id)
        {
            var bookDetails = await _context.Books
                .Include(c => c.Shop)
                .Include(p => p.Publisher)
                .Include(a => a.Author)
                .FirstOrDefaultAsync(n => n.Id == id);
             
            return bookDetails;
        }

        public async Task<NewBookDropdownsVM> GetNewBookDropdownsValues()
        {
            var response = new NewBookDropdownsVM()
            {
                Authors = await _context.Authors.OrderBy(n => n.Name).ToListAsync(),
                Shops = await _context.Shops.OrderBy(n => n.ShopName).ToListAsync(),
                Publishers = await _context.Publishers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateBookAsync(NewBookVM data)
        {
            var dbBook = await _context.Books.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbBook != null)
            {
                dbBook.BookName = data.Name;
                dbBook.Overview = data.Overview;
                dbBook.Price = data.Price;
                dbBook.ImageURL = data.ImageURL;
                dbBook.ShopId = data.ShopId;
                dbBook.Category = data.Bookcategory;
                dbBook.PublisherId = data.PublisherId;
                dbBook.AuthorId = data.AuthorId;
                await _context.SaveChangesAsync();
            }

            //Remove existing authors
           // var existingAuthorsDb = _context.Author_Books.Where(n => n.BookId == data.Id).ToList();
           // _context.Author_Books.RemoveRange(existingAuthorsDb);
            //await _context.SaveChangesAsync();

            //Add Book Auhtors
            //foreach (var authorId in data.AuthorIds)
            //{
              //  var newAuthorBook = new Author_Books()
                //{
                  //  BookId = data.Id,
                    //AuthorId = authorId
                //};
                //await _context.Author_Books.AddAsync(newAuthorBook);
            //}
            //await _context.SaveChangesAsync();
        }
    }
}
