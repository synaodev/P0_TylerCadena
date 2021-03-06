using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using TylerMart.Domain.Models;
using TylerMart.Storage.Contexts;

namespace TylerMart.Storage.Repositories {
	/// <summary>
	/// Generic repository interface for <see cref="TylerMart.Domain.Models.Model"/>
	/// </summary>
	public interface IRepository<T> where T : Model {
		/// <summary>
		/// Get all rows
		/// </summary>
		/// <returns>
		/// List of models
		/// </returns>
		List<T> All();
		/// <summary>
		/// Check for row with primary key
		/// </summary>
		/// <returns>
		/// 'true' if row exists
		/// </returns>
		bool Exists(int ID);
		/// <summary>
		/// Get a row with primary key
		/// </summary>
		/// <param name="ID">Primary Key</param>
		/// <returns>
		/// Single row or null
		/// </returns>
		T Get(int ID);
		/// <summary>
		/// Create new row using model data
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully inserted into database
		/// </returns>
		bool Create(T model);
		/// <summary>
		/// Update existing row using model data
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully updated in database
		/// </returns>
		bool Update(T model);
		/// <summary>
		/// Remove existing row with model's primary key
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully removed from database
		/// </returns>
		bool Delete(T model);
	}
	/// <summary>
	/// Generic repository interface for <see cref="TylerMart.Domain.Models.Model"/>
	/// </summary>
	public abstract class Repository<T> : IRepository<T> where T : Model {
		/// <summary>
		/// Instance of DatabaseContext used for database access
		/// </summary>
		protected DatabaseContext Db;
		/// <summary>
		/// Constructor that takes an instance of DbContext
		/// </summary>
		/// <param name="db">Instance of DatabaseContext</param>
		public Repository(DatabaseContext db) {
			Db = db;
		}
		/// <summary>
		/// Get all rows
		/// </summary>
		/// <returns>
		/// List of models
		/// </returns>
		public List<T> All() => Db.Set<T>().ToList();
		/// <summary>
		/// Check for row with primary key
		/// </summary>
		/// <returns>
		/// 'true' if row exists
		/// </returns>
		public bool Exists(int ID) => Db.Set<T>().Any(m => m.ID == ID);
		/// <summary>
		/// Get a row with primary key
		/// </summary>
		/// <param name="ID">Primary Key</param>
		/// <returns>
		/// Single row or null
		/// </returns>
		public T Get(int ID) => Db.Set<T>().SingleOrDefault(m => m.ID == ID);
		/// <summary>
		/// Update existing row using model data
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully updated in database
		/// </returns>
		public bool Create(T model) {
			Db.Set<T>().Add(model);
			return this.Commit();
		}
		/// <summary>
		/// Update existing row using model data
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully updated in database
		/// </returns>
		public bool Update(T model) {
			Db.Set<T>().Update(model);
			return this.Commit();
		}
		/// <summary>
		/// Remove existing row with model's primary key
		/// </summary>
		/// <param name="model">Model data</param>
		/// <returns>
		/// 'true' if successfully removed from database
		/// </returns>
		public bool Delete(T model) {
			Db.Set<T>().Remove(model);
			return this.Commit();
		}
		/// <summary>
		/// Attempts to save changes and to roll them back if something goes wrong
		/// </summary>
		/// <returns>
		/// 'true' if change to the database was successful
		/// </returns>
		protected virtual bool Commit() {
			bool success = true;
			try {
				Db.SaveChanges();
			} catch (DbUpdateException) { // Expected
				success = false;
			} catch (Exception) { // Unexpected
				success = false;
				throw;
			} finally {
				if (!success) {
					Db.ChangeTracker.Entries().ToList().ForEach(e => {
						switch (e.State) {
						case EntityState.Modified:
							e.State = EntityState.Unchanged;
							break;
						case EntityState.Added:
							e.State = EntityState.Detached;
							break;
						case EntityState.Deleted:
							e.Reload();
							break;
						default:
							break;
						}
					});
				}
			}
			return success;
		}
	}
}
