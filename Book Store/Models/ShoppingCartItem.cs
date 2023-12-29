using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store.Models
{
	public class ShoppingCartItem
	{
		[Key]
		public int Id { get; set; }

		public Books Book { get; set; }
		public int Amount { get; set; }

		[ForeignKey("ShoppingCartId")]
		public int ShoppingCartId { get; set; }
		public ShoppingCart ShoppingCart { get; set; }

	}
}
