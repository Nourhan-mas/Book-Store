using Book_Store.Models;
using Publisher = Book_Store.Models.Publisher;

namespace Book_Store.Data.ViewModels
{
    public class NewBookDropdownsVM
    {
        public NewBookDropdownsVM()
        {
            Publishers = new List<Publisher>();
            Shops = new List<Shop>();
            Authors = new List<Author>();
        }

        public List<Publisher> Publishers { get; set; }
        public List<Shop> Shops { get; set; }
        public List<Author> Authors { get; set; }
    }
}
