using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// <see cref="TylerMart.Domain.Models.Customer"/> Repository
	/// </summary>
	public class CustomerRepository : Repository<Customer> {
		public CustomerRepository(DbContext db) : base(db) {}
		/// <summary>
		/// Gets Customer from email address
		/// </summary>
		/// <param name="emailAddress">Email Address</param>
		/// <returns>
		/// Single Customer or null
		/// </returns>
		public Customer GetByEmailAddress(string emailAddress) {
			return Db.Set<Customer>()
				.SingleOrDefault(c => c.EmailAddress == emailAddress);
		}
		/// <summary>
		/// Finds Customers with first name
		/// </summary>
		/// <param name="name">First name</param>
		/// <returns>
		/// List of Customers
		/// </returns>
		public List<Customer> FindByFirstName(string name) {
			return Db.Set<Customer>()
				.Where(c => c.FirstName == name)
				.ToList();
		}
		/// <summary>
		/// Finds Customers with last name
		/// </summary>
		/// <param name="name">Last name</param>
		/// <returns>
		/// List of Customers
		/// </returns>
		public List<Customer> FindByLastName(string name) {
			return Db.Set<Customer>()
				.Where(c => c.LastName == name)
				.ToList();
		}
		/// <summary>
		/// Finds Customers with both first and last names
		/// </summary>
		/// <param name="firstName">First name</param>
		/// <param name="lastName">Last name</param>
		/// <returns>
		/// List of Customers
		/// </returns>
		public List<Customer> FindByWholeName(string firstName, string lastName) {
			return Db.Set<Customer>()
				.Where(c => c.FirstName == firstName && c.LastName == lastName)
				.ToList();
		}
	}
}
