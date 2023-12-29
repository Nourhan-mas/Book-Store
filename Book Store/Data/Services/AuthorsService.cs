using Book_Store.Data.Base;
using Book_Store.Data.Services.Interfaces;
using Book_Store.Models;

namespace Book_Store.Data.Services
{
    public class AuthorsService : EntityBaseRepository<Author>, IAuthorsService
    {
        public AuthorsService(AppDbContext context) : base(context) { }
    }
}
