using Book_Store.Data.Base;
using Book_Store.Data.ViewModels;
using Book_Store.Models;

namespace Book_Store.Data.Services.Interfaces
{
    public interface IBooksService : IEntityBaseRepository<Books>
    {
        Task<Books> GetBookByIdAsync(int id);
        Task<NewBookDropdownsVM> GetNewBookDropdownsValues();
        Task AddNewBookAsync(NewBookVM data);
        Task UpdateBookAsync(NewBookVM data);
    }
}
