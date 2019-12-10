using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Wto.Services.Members.Service.Controllers {
	public abstract class BaseController : ControllerBase {
		#region -- Variable Declarations --

		private IMediator _mediator;

		#endregion

		#region -- Properties --

		protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

		#endregion
	}
}