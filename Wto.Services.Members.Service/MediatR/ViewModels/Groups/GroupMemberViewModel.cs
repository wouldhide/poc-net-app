using System;

namespace Wto.Services.Members.Service.MediatR.ViewModels.Groups {
	public class GroupMemberViewModel {
		#region -- Properties --

		public string Id { get; set; }
		public string Name { get; set; }
		public string ISOCode { get; set; }
		public int DisplayOrder { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool IsActive { get; set; }

		#endregion
	}
}