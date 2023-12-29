using Book_Store.Data.Base;
using Book_Store.Data.Services.Interfaces;
using Book_Store.Models;

namespace Book_Store.Data.Services
{
    public class ShopsService : EntityBaseRepository<Shop>, IShopsService
    {
        public ShopsService(AppDbContext context) : base(context)
        {
        }
    }
}
