using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wto.Services.Members.Service.DataModels {
	[Table("Country Prefix Code")]
	public class CountryPrefix {
		#region -- Prefix --

		[Key, Column("Country Prefix Code")]
		public string Id { get; set; }

		[Column("Description")]
		public string Name { get; set; }

		#endregion
	}
}