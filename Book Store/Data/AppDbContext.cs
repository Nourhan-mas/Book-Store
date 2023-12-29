using Book_Store.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Author> Authors { get; set; }
		public DbSet<Books> Books { get; set; }
		public DbSet<Shop> Shops { get; set; }
		public DbSet<Publisher> Publishers { get; set; }


		//Orders related tables
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<ShoppingCart> ShoppingCart { get; set; }
		public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
	
	}
}
