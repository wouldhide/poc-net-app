using System.Reflection;
using AutoMapper;
using Wto.Library.Core.AutoMapper;

namespace Wto.Services.Members.Service {
	public class AutoMapperProfile : Profile {
		#region -- Constructor --

		public AutoMapperProfile() {
			LoadStandardMappings();
			LoadCustomMappings();
			LoadConverters();
		}

		#endregion

		#region -- Functions --

		private void LoadConverters() {

		}

		private void LoadStandardMappings() {
			var mapsFrom = AutoMapperHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

			foreach (var map in mapsFrom) {
				CreateMap(map.Source, map.Destination).ReverseMap();
			}
		}

		private void LoadCustomMappings() {
			var mapsFrom = AutoMapperHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

			foreach (var map in mapsFrom) {
				map.CreateMappings(this);
			}
		}


		#endregion
	}
}