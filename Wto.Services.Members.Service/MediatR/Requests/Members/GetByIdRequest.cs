using MediatR;
using Wto.Library.Core.Application.Requests;
using Wto.Services.Members.Service.MediatR.ViewModels.Members;

namespace Wto.Services.Members.Service.MediatR.Requests.Members {
	public class GetByIdRequest : BaseLocalizedRequest, IRequest<MemberViewModel> {
		#region -- Properties --

		public string Id { get; set; }

		#endregion
	}
}