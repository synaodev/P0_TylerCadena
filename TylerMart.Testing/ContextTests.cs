using System;
using System.Collections.Generic;
using Xunit;

using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;
using TylerMart.Testing.Services;

namespace TylerMart.Testing {
	/// <summary>
	/// Tests for <see cref="TylerMart.Storage.Contexts.DatabaseContext"/>
	/// </summary>
	public class ContextTests {
		/// <summary>
		/// Checks for seeded data in database
		/// </summary>
		[Fact]
		public void TestSeededData() {
			DatabaseService db = new DatabaseService();

			List<Customer> customerList = db.Customers.All();
			bool customerExists = db.Customers.Exists(1);
			Customer customer = db.Customers.Get(1);

			Assert.NotEmpty(customerList);
			Assert.True(customerExists);
			Assert.NotNull(customer);

			List<Product> productList = db.Products.All();
			bool productExists = db.Products.Exists(1);
			Product product = db.Products.Get(1);

			Assert.NotEmpty(productList);
			Assert.True(productExists);
			Assert.NotNull(product);

			List<Location> locationList = db.Locations.All();
			bool locationExists = db.Locations.Exists(1);
			Location location = db.Locations.Get(1);

			Assert.NotEmpty(locationList);
			Assert.True(locationExists);
			Assert.NotNull(location);

			List<Order> orderList = db.Orders.All();
			bool orderExists = db.Orders.Exists(1);
			Order order = db.Orders.Get(1);

			Assert.NotEmpty(orderList);
			Assert.True(orderExists);
			Assert.NotNull(order);
		}
		/// <summary>
		/// Checks for seeded many-to-many relationships in database
		/// </summary>
		[Fact]
		public void TestSeededManyToManyData() {
			DatabaseService db = new DatabaseService();

			Location location = db.Locations.Get(1);
			List<Product> productsAtLocation = db.Products.FindFromLocation(location);
			Assert.NotEmpty(productsAtLocation);

			Order order = db.Orders.Get(1);
			List<Product> productsInOrder = db.Products.FindFromOrder(order);
			Assert.NotEmpty(productsInOrder);
		}
		/// <summary>
		/// Checks to ensure unique fields are actually unique
		/// </summary>
		[Fact]
		public void TestUniqueFields() {
			DatabaseService db = new DatabaseService();

			Action attempt1 = new Action(delegate() {
				db.Customers.Create(new Customer() {
					FirstName = "Tyler2",
					LastName = "Cadena2",
					// this email exists in seeded data
					EmailAddress = "tyler.cadena@revature.net",
					Password = "tyler2cadena2"
				});
			});
			Action attempt2 = new Action(delegate() {
				Customer c = db.Customers.Get(2);
				c.EmailAddress = "tyler.cadena@revature.net";
				db.Customers.Update(c);
			});
			Assert.Throws<DbUpdateException>(attempt2);

			Action attempt3 = new Action(delegate() {
				db.Locations.Create(new Location() {
					Name = "Florida"
				});
			});
			Assert.Throws<DbUpdateException>(attempt3);

			Action attempt4 = new Action(delegate() {
				Location l = db.Locations.Get(1);
				l.Name = "Florida";
				db.Locations.Update(l);
			});
			Assert.Throws<DbUpdateException>(attempt4);
		}
	}
}