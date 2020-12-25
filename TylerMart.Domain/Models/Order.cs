using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Order Model contains:
	/// <list>
	/// <item>- Order ID</item>
	/// <item>- Placement Date</item>
	/// <item>- Completed</item>
	/// <item>- The <see cref="TylerMart.Domain.Models.Customer"/></item>
	/// <item>- The <see cref="TylerMart.Domain.Models.Location"/></item>
	/// <item>- List of <see cref="TylerMart.Domain.Models.OrderProduct"/> Pairs</item>
	/// </list>
	/// </summary>
	[Table("Orders")]
	public class Order : Model {
		[Key]
		public int OrderID { get; set; }
		public DateTime PlacedAt { get; set; }
		public bool Completed { get; set; }
		[ForeignKey("Customer")]
		public int CustomerID { get; set; }
		public virtual Customer Customer { get; set; }
		[ForeignKey("Location")]
		public int LocationID { get; set; }
		public virtual Location Location { get; set; }
		public virtual List<OrderProduct> OrderProducts { get; set; }
		public override int GetID() => OrderID;
		/// <summary>
		/// Generates Order array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Orders
		/// </returns>
		public static Order[] GenerateSeededData() {
			Customer[] customers = Customer.GenerateSeededData();
			Location[] locations = Location.GenerateSeededData();
			Order[] orders = new Order[] {
				new Order() {
					OrderID = 1,
					PlacedAt = DateTime.Now,
					Completed = false,
					CustomerID = customers[0].CustomerID,
					LocationID = locations[0].LocationID
				},
				new Order() {
					OrderID = 2,
					PlacedAt = DateTime.Now,
					Completed = false,
					CustomerID = customers[1].CustomerID,
					LocationID = locations[1].LocationID
				}
			};
			return orders;
		}
	}
}
