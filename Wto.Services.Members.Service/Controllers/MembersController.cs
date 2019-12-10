using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wto.Services.Members.Service.MediatR.Requests.Members;

namespace Wto.Services.Members.Service.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class MembersController : BaseController {
		#region -- Apis --

		[HttpGet(Name = "GetMembers")]
		public async Task<IActionResult> Get([FromQuery] GetRequest request) {
			var model = await Mediator.Send(request);
			return new OkObjectResult(model);
		}

		[HttpGet("{id}", Name = "GetMemberById")]
		public async Task<IActionResult> GetById(string id, [FromQuery] GetByIdRequest request) {
			request.Id = id;
			var model = await Mediator.Send(request);
			return new OkObjectResult(model);
		}

		#endregion
	}
}