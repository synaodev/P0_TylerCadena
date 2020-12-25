using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TylerMart.Domain.Models {
	/// <summary>
	/// Customer Model contains:
	/// <list>
	/// <item>- Customer ID</item>
	/// <item>- First Name</item>
	/// <item>- Last Name</item>
	/// <item>- Email Address</item>
	/// <item>- Password</item>
	/// </list>
	/// </summary>
	[Table("Customers")]
	public class Customer : Model {
		[Key]
		public int CustomerID { get; set; }
		[MinLength(2)]
		public string FirstName { get; set; }
		[MinLength(2)]
		public string LastName { get; set; }
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public override int GetID() => CustomerID;
		/// <summary>
		/// Generates Customer array for seeding database
		/// </summary>
		/// <remarks>
		/// Only to be used in <c>DbContext.OnModelCreating()</c>
		/// </remarks>
		/// <returns>
		/// Array of Customers
		/// </returns>
		public static Customer[] GenerateSeededData() {
			Customer[] customers = new Customer[] {
				new Customer() {
					CustomerID = 1,
					FirstName = "Tyler",
					LastName = "Cadena",
					EmailAddress = "tyler.cadena@revature.net",
					Password = "synaodev"
				},
				new Customer() {
					CustomerID = 2,
					FirstName = "George",
					LastName = "Bumble",
					EmailAddress = "george.bumble@revature.net",
					Password = "onionbutt"
				}
			};
			return customers;
		}
	}
}
