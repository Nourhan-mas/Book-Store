using Book_Store.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    public class Author : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string PictureURL { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        //relations 
        public List<Books> Books { get; set; }

    }
}
