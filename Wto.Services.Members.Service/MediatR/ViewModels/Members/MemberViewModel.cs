using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Wto.Library.Core;
using Wto.Library.Core.AutoMapper;
using Wto.Services.Members.Service.DataModels;

namespace Wto.Services.Members.Service.MediatR.ViewModels.Members {
	public class MemberViewModel : IMapping {
		#region -- Properties --

		public string Id { get; set; }
		public string Name { get; set; }
		public string ISOCode { get; set; }
		public int DisplayOrder { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool IsActive { get; set; }

		public IEnumerable<MemberGroupViewModel> Groups { get; set; }

		#endregion

		#region -- IMapping Members --

		public void CreateMappings(Profile configuration) {
			var language = WtoEnums.Language.English;
			var now = DateTime.Now;

			configuration.CreateMap<Country, MemberViewModel>()
				.ForMember(t => t.ISOCode, option => option.MapFrom(src => src.CountryCode.ISOCode))
				.ForMember(t => t.StartDate, option => option.MapFrom(src => src.Groups.FirstOrDefault(cg => cg.GroupId == WtoConstants.WtoMemberGroupId).StartDate))
				.ForMember(t => t.EndDate, option => option.MapFrom(src => src.Groups.FirstOrDefault(cg => cg.GroupId == WtoConstants.WtoMemberGroupId).EndDate))
				.ForMember(
					t => t.Groups, 
					option => option.MapFrom(src => 
						src.Groups.Where(m => 
							m.Group.Language == language
						)
						.Select(m => new MemberGroupViewModel {
							Id = m.Group.Id,
							Name = m.Group.Name,
							ISOCode = m.Group.CountryCode.ISOCode,
							DisplayOrder = m.Group.DisplayOrder,
							StartDate = m.StartDate,
							EndDate = m.EndDate,
							IsActive = (m.StartDate ?? now) <= now && (m.EndDate ?? now) >= now
						})
						.OrderBy(m => m.DisplayOrder)
					)
				)
			;
		}

		#endregion
	}
}