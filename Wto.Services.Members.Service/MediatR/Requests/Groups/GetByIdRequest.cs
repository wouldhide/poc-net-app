using MediatR;
using Wto.Library.Core.Application.Requests;
using Wto.Services.Members.Service.MediatR.ViewModels.Groups;

namespace Wto.Services.Members.Service.MediatR.Requests.Groups {
	public class GetByIdRequest : BaseLocalizedRequest, IRequest<GroupViewModel> {
		#region -- Properties --

		public string Id { get; set; }

		#endregion
	}
}