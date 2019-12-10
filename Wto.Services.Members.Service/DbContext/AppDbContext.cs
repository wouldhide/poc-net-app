using System.Linq;
using Microsoft.EntityFrameworkCore;
using Wto.Library.Core;
using Wto.Services.Members.Service.DataModels;

namespace Wto.Services.Members.Service.DbContext {
	public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext {
		#region -- Db Sets --

		public DbSet<Country> Countries { get; set; }
		public DbSet<CountryGroup> CountryGroups { get; set; }
		public DbSet<CountryPrefix> CountryPrefixes { get; set; }

		#endregion

		#region -- Extended Db Sets --

		public IQueryable<Country> Members => this.Countries.GetMembers();

		#endregion

		#region -- Constructor --

		public AppDbContext(DbContextOptions options) : base(options) {

		}

		#endregion

		#region -- Overrides --

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<CountryGroup>()
				.HasKey(t => new {t.GroupId, t.MemberId});

			modelBuilder.Entity<CountryGroup>()
				.HasOne(t => t.Group)
				.WithMany(t => t.Members)
				.HasForeignKey(t => t.GroupId);
			
			modelBuilder.Entity<CountryGroup>()
				.HasOne(t => t.Member)
				.WithMany(t => t.Groups)
				.HasForeignKey(t => t.MemberId);
		}

		#endregion
	}
}