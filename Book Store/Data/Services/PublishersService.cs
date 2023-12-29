using Book_Store.Data.Base;
using Book_Store.Data.Services.Interfaces;
using Book_Store.Models;

namespace Book_Store.Data.Services
{
    public class PublishersService : EntityBaseRepository<Publisher>, IPublishersService
    {
        public PublishersService(AppDbContext context) : base(context)
        {
        }
    }
}
