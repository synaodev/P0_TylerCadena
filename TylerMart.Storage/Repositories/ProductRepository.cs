using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;

namespace TylerMart.Storage.Repositories {
	public class ProductRepository : Repository<Product> {
		public ProductRepository(DbContext db) : base(db) {}
		/// <summary>
		/// Gets list of products in an order
		/// </summary>
		/// <returns>
		/// Returns list of products
		/// </returns>
		public List<Product> FindFromOrder(Order order) {
			return Db.Set<OrderProduct>()
				.Where(op => op.OrderID == order.OrderID)
				.Include(op => op.Product)
				.Select(op => op.Product)
				.ToList();
		}
		/// <summary>
		/// Gets list of products in a location's inventory
		/// </summary>
		/// <returns>
		/// Returns list of products
		/// </returns>
		public List<Product> FindFromLocation(Location location) {
			return Db.Set<LocationProduct>()
				.Where(lp => lp.LocationID == location.LocationID)
				.Include(lp => lp.Product)
				.Select(lp => lp.Product)
				.ToList();
		}
	}
}
