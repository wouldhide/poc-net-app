using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Wto.Library.Core;
using Wto.Library.Core.Application.Requests;
using Wto.Services.Members.Service.MediatR.ViewModels.Groups;

namespace Wto.Services.Members.Service.MediatR.Requests.Groups {
	public class GetRequest : BaseLocalizedListRequest, IRequest<GetViewModel>, IValidatableObject {
		#region -- Properties --

		[Description("(A) Aggregate\r\n(C) Country or territory\r\n(F) FTA\r\n(G) Geographical region\r\n(N) Not elsewhere specified\r\n(R) Geographical sub-region\r\n(S) Geographical sub-region\r\n(U) Customs Union\r\n(X) Other")]
		public WtoEnums.GroupTypes? Type { get; set; }		

		#endregion

		#region -- IValidatableObject Members --

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
			if (!this.Type.HasValue) {
				yield return new ValidationResult("Please specify type of groups you want to retrieve.", new[] {"Type"});
			}
		}

		#endregion
	}
}