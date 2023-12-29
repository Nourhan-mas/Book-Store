using Book_Store.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    public class Shop : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Shop Logo")]
        [Required(ErrorMessage = "Shop logo is required")]
        public string ShopLogo { get; set; }

        [Display(Name = "Shop Name")]
        [Required(ErrorMessage = "Shop name is required")]
        public string ShopName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Shop description is required")]
        public string Description { get; set; }

        //relations 
        public List<Books> Books { get; set; }
    }
}
