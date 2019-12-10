using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wto.Services.Members.Service.DbContext;
using Wto.Services.Members.Service.MediatR.Requests.Members;
using Wto.Services.Members.Service.MediatR.ViewModels.Members;

namespace Wto.Services.Members.Service.MediatR.RequestHandlers.Members {
	public class GetByIdRequestHandler : IRequestHandler<GetByIdRequest, MemberViewModel> {
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

		public async Task<MemberViewModel> Handle(GetByIdRequest request, CancellationToken cancellationToken) {
			var country = await _db.Members
				.Where(t => t.Id == request.Id && t.Language == request.Language)
				.ProjectTo<MemberViewModel>(_configurationProvider, new {language = request.Language})
				.FirstOrDefaultAsync(cancellationToken: cancellationToken);

			return country;
		}
		

		#endregion
	}
}