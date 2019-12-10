using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Wto.Services.Members.Service.DataModels {
	[Table("Country Group")]
	public class CountryGroup {
		#region -- Properties --

		[ForeignKey("Group")]
		[Key, Column("WTO Group Code", Order = 1)]
		public string GroupId { get; set; }
		[JsonIgnore]
		public virtual Country Group { get; set; }

		[ForeignKey("Member")]
		[Key, Column("WTO Member Code", Order = 2)]
		public string MemberId { get; set; }
		[JsonIgnore]
		public virtual Country Member { get; set; }

		[Column("Start date")]
		public DateTime? StartDate { get; set; }

		[Column("End date")]
		public DateTime? EndDate { get; set; }

		#endregion
	}
}