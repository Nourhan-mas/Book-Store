using Book_Store.Data.Base;
using Book_Store.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store.Models
{
    public class Books : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Overview { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public Bookcategory Category { get; set; }
        //relations 

        //Shop
        public int ShopId { get; set; }
        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
        //Publisher
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

    }
}
