using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store.Models
{
	public class ShoppingCart
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public ApplicationUser User { get; set; }
		public List<ShoppingCartItem> ShoppingCartItems { get; set; }

	}
}
