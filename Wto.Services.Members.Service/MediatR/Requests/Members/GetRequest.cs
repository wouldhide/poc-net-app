using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Wto.Library.Core.Application.Requests;
using Wto.Services.Members.Service.MediatR.ViewModels.Members;

namespace Wto.Services.Members.Service.MediatR.Requests.Members {
	public class GetRequest : BaseLocalizedListRequest, IRequest<GetViewModel>, IValidatableObject {
		#region -- Properties --

		#endregion

		#region -- IValidatableObject Members --

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
			if (this.Page <= 0) {
				yield return new ValidationResult("Invalid Page parameter", new[] {"Page"});
			}
		}

		#endregion
	}
}