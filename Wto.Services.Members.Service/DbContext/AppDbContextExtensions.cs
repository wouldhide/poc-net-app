using System.Linq;
using Wto.Library.Core;
using Wto.Services.Members.Service.DataModels;

namespace Wto.Services.Members.Service.DbContext {
	public static class AppDbContextExtensions {
		#region -- Extensions --

		public static IQueryable<Country> GetMembers(this IQueryable<Country> countries) {
			return countries.Where(t =>
				t.Groups.Any(cg => WtoConstants.ValidGroups.Contains(cg.GroupId))
				&& !WtoConstants.InvalidPrefixes.Contains(t.Prefix)
				&& !WtoConstants.InvalidIds.Contains(t.Id)
			);

		}

		#endregion
	}
}