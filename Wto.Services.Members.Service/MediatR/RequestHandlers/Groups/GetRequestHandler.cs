using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wto.Library.Core;
using Wto.Library.Core.Application.Requests;
using Wto.Services.Members.Service.DbContext;
using Wto.Services.Members.Service.MediatR.Requests.Groups;
using Wto.Services.Members.Service.MediatR.ViewModels.Groups;

namespace Wto.Services.Members.Service.MediatR.RequestHandlers.Groups {
	public class GetRequestHandler : IRequestHandler<GetRequest, GetViewModel> {
		#region -- Variable Declarations --

		private readonly AppDbContext _db;
		private readonly IConfigurationProvider _configurationProvider;

		#endregion

		#region -- Constructor --

		public GetRequestHandler(AppDbContext db, IConfigurationProvider configurationProvider) {
			_db = db;
			_configurationProvider = configurationProvider;
		}

		#endregion

		#region -- IRequestHandler Members --

		public async Task<GetViewModel> Handle(GetRequest request, CancellationToken cancellationToken) {
			var groups = _db.Countries
				.Where(t => t.Prefix == WtoConstants.GroupTypes[(int)request.Type] && t.Language == request.Language)
				.ProjectTo<GroupViewModel>(_configurationProvider, new {language = request.Language});

			if (!string.IsNullOrEmpty(request.SearchTerm)) {
				groups = groups.Where(t => t.Name.Contains(request.SearchTerm) || t.Id.Contains(request.SearchTerm));
			}

			switch (request.SortBy) {
				case "Id":
					groups = ((request.SortDirection ?? WtoEnums.SortDirection.Asc) == WtoEnums.SortDirection.Asc ? groups.OrderBy(t => t.Id) : groups.OrderByDescending(t => t.Id)).AsQueryable();
					break;
				case "Name":
					groups = ((request.SortDirection ?? WtoEnums.SortDirection.Asc) == WtoEnums.SortDirection.Asc ? groups.OrderBy(t => t.Name) : groups.OrderByDescending(t => t.Name)).AsQueryable();
					break;
				default:
					groups = ((request.SortDirection ?? WtoEnums.SortDirection.Asc) == WtoEnums.SortDirection.Asc ? groups.OrderBy(t => t.DisplayOrder) : groups.OrderByDescending(t => t.DisplayOrder)).AsQueryable();
					break;
			}

			int totalPages;
			IQueryable<GroupViewModel> items;

			var count = await groups.CountAsync(cancellationToken: cancellationToken);
			var additionalPage = count % request.ItemsPerPage == 0 ? 0 : 1;
			var itemsPerPage = request.ItemsPerPage ?? BaseLocalizedListRequest.DefaultItemsPerPage;
			var page = request.Page ?? 1;

			if (request.GetAll) {
				totalPages = 1;
				itemsPerPage = count;
				items = groups;
			}
			else {
				totalPages = ((int) count / request.ItemsPerPage ?? BaseLocalizedListRequest.DefaultItemsPerPage) + additionalPage;
				items = groups.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable();
			}

			var model = new GetViewModel {
				Items = await items.ToArrayAsync(cancellationToken),
				Page = page,
				ItemsPerPage = itemsPerPage,
				TotalPages = totalPages,
				TotalItems = count
			};

			return model;
		}

		#endregion
	}
}