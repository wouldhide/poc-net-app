using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Wto.Library.Core;

namespace Wto.Services.Members.Service.DataModels {
	[Table("Country Code")]
	public class CountryCode {
		#region -- Properties --

		[ForeignKey("Country")]
		[Key, Column("WTO Country Code")]
		public string Id { get; set; }
		[JsonIgnore]
		public virtual Country Country { get; set; }

		[Column("Is Active")]
		public string IsActive { get; set; }

		[Column("ISO-3A")]
		public string ISOCode { get; set; }

		[Column("Start date")]
		public DateTime? StartDate { get; set; }

		[Column("End date")]
		public DateTime? EndDate { get; set; }

		#endregion
	}
}