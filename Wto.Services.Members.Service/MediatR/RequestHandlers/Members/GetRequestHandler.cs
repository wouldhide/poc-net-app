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
using Wto.Services.Members.Service.MediatR.Requests.Members;
using Wto.Services.Members.Service.MediatR.ViewModels.Members;

namespace Wto.Services.Members.Service.MediatR.RequestHandlers.Members {
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
			var countries = _db.Members
				.Where(t => t.Language == request.Language)
				.ProjectTo<MemberViewModel>(_configurationProvider);

			if (!string.IsNullOrEmpty(request.SearchTerm)) {
				countries = countries.Where(t => t.Name.Contains(request.SearchTerm) || t.Id.Contains(request.SearchTerm));
			}

			switch (request.SortBy) {
				case "Id":
					countries = ((request.SortDirection ?? WtoEnums.SortDirection.Asc) == WtoEnums.SortDirection.Asc ? countries.OrderBy(t => t.Id) : countries.OrderByDescending(t => t.Id)).AsQueryable();
					break;
				case "Name":
					countries = ((request.SortDirection ?? WtoEnums.SortDirection.Asc) == WtoEnums.SortDirection.Asc ? countries.OrderBy(t => t.Name) : countries.OrderByDescending(t => t.Name)).AsQueryable();
					break;
				default:
					countries = ((request.SortDirection ?? WtoEnums.SortDirection.Asc) == WtoEnums.SortDirection.Asc ? countries.OrderBy(t => t.DisplayOrder) : countries.OrderByDescending(t => t.DisplayOrder)).AsQueryable();
					break;
			}

			int totalPages;
			IQueryable<MemberViewModel> items;

			var count = await countries.CountAsync(cancellationToken: cancellationToken);
			var additionalPage = count % request.ItemsPerPage == 0 ? 0 : 1;
			var itemsPerPage = request.ItemsPerPage ?? BaseLocalizedListRequest.DefaultItemsPerPage;
			var page = request.Page ?? 1;

			if (request.GetAll) {
				totalPages = 1;
				itemsPerPage = count;
				items = countries;
			}
			else {
				totalPages = ((int) count / request.ItemsPerPage ?? BaseLocalizedListRequest.DefaultItemsPerPage) + additionalPage;
				items = countries.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).AsQueryable();
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