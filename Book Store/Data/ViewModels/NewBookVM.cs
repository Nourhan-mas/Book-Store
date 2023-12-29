using Book_Store.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    public class NewBookVM
    {
        public int Id { get; set; }

        [Display(Name = "Book name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Book Overview")]
        [Required(ErrorMessage = "Overview is required")]
        public string Overview { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Book poster URL")]
        [Required(ErrorMessage = "Book poster URL is required")]
        public string ImageURL { get; set; }


        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Book category is required")]
        public Bookcategory Bookcategory { get; set; }

        //Relationships
        [Display(Name = "Select an author")]
        [Required(ErrorMessage = "Book Author is required")]
        public int AuthorId { get; set; }

        [Display(Name = "Select a Shop")]
        [Required(ErrorMessage = "Book Shop is required")]
        public int ShopId { get; set; }

        [Display(Name = "Select a publisher")]
        [Required(ErrorMessage = "Book publisher is required")]
        public int PublisherId { get; set; }
    }
}
