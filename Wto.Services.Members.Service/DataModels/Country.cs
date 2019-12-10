using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wto.Library.Core;
using Wto.Services.Common;

namespace Wto.Services.Members.Service.DataModels {
	[Table("Country Name")]
	public class Country {
		#region -- Properties --

		[Key, Column("WTO Country Code")]
		public string Id { get; set; }

		[Column("Lang")]
		public WtoEnums.Language Language { get; set; }

		[Column("Code")]
		public string Code { get; set; }

		[Column("Name")]
		public string Name { get; set; }

		[Column("Display Order")]
		public int DisplayOrder { get; set; }

		[Column("Code Prefix")]
		public string Prefix { get; set; }

		public virtual ICollection<CountryGroup> Groups { get; set; }

		public virtual ICollection<CountryGroup> Members { get; set; }

		public virtual CountryCode CountryCode { get; set; }

		#endregion
	}
}
