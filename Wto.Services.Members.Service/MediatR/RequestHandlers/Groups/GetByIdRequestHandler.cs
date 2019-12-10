using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wto.Services.Members.Service.DbContext;
using Wto.Services.Members.Service.MediatR.Requests.Groups;
using Wto.Services.Members.Service.MediatR.ViewModels.Groups;

namespace Wto.Services.Members.Service.MediatR.RequestHandlers.Groups {
	public class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, GroupViewModel> {
		#region -- Variable Declarations --

		private readonly AppDbContext _db;
		private readonly IConfigurationProvider _configurationProvider;

		#endregion

		#region -- Constructor --

		public GetByIdRequestHandler(AppDbContext db, IConfigurationProvider configurationProvider) {
			_db = db;
			_configurationProvider = configurationProvider;
		}

		#endregion

		#region -- IRequestHandler Members --

		public async Task<GroupViewModel> Handle(GetByIdRequest request, CancellationToken cancellationToken) {
			var group = await _db.Countries
				.Where(t => t.Id == request.Id && t.Language == request.Language)
				.ProjectTo<GroupViewModel>(_configurationProvider, new {language = request.Language})
				.FirstOrDefaultAsync(cancellationToken: cancellationToken);

			return group;
		}

		#endregion
	}
}