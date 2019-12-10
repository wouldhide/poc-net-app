using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Wto.Library.Core;
using Wto.Library.Core.AutoMapper;
using Wto.Services.Members.Service.DataModels;

namespace Wto.Services.Members.Service.MediatR.ViewModels.Groups {
	public class GroupViewModel : IMapping {
		#region -- Properties --

		public string Id { get; set; }
		public string Name { get; set; }
		public string ISOCode { get; set; }
		public int DisplayOrder { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool IsActive { get; set; }
		public IEnumerable<GroupMemberViewModel> Members { get; set; }

		#endregion

		#region -- IMapping Members --

		public void CreateMappings(Profile configuration) {
			var language = WtoEnums.Language.English;
			var now = DateTime.Now;

			configuration.CreateMap<Country, GroupViewModel>()
				.ForMember(t => t.ISOCode, option => option.MapFrom(src => src.CountryCode.ISOCode))
				.ForMember(t => t.StartDate, option => option.MapFrom(src => src.CountryCode.StartDate))
				.ForMember(t => t.EndDate, option => option.MapFrom(src => src.CountryCode.EndDate))
				.ForMember(t => t.IsActive, option => option.MapFrom(src => (src.CountryCode.StartDate ?? now) <= now && (src.CountryCode.EndDate ?? now) >= now))
				.ForMember(
					t => t.Members, 
					option => option.MapFrom(src => 
						src.Members.Where(m => 
							m.Member.Language == language
							&& m.Member.Groups.Any(cg => WtoConstants.ValidGroups.Contains(cg.GroupId))
							&& !WtoConstants.InvalidPrefixes.Contains(m.Member.Prefix)
							&& !WtoConstants.InvalidIds.Contains(m.Member.Id)
						)
						.Select(m => new GroupMemberViewModel {
							Id = m.Member.Id,
							Name = m.Member.Name,
							ISOCode = m.Member.CountryCode.ISOCode,
							DisplayOrder = m.Member.DisplayOrder,
							StartDate = m.StartDate,
							EndDate = m.EndDate,
							IsActive = (m.StartDate ?? now) <= now && (m.EndDate ?? now) >= now
						})
						.OrderBy(m => m.DisplayOrder)
					)
				);
		}

		#endregion
	}
}