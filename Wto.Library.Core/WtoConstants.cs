using System.Collections.Generic;

namespace Wto.Library.Core {
	public class WtoConstants {
		public const string WtoMemberGroupId = "A900";

		public const string DevelopedCountriesGroupId = "A995";
		public const string DevelopingCountriesGroupId = "A996";
		public const string LeastDevelopedCountriesGroupId = "A997";

		public static readonly string[] InvalidPrefixes = new[] { "N" };
		public static readonly string[] InvalidIds = new[] { "C899" };
		public static readonly string[] ValidGroups = new[] { WtoConstants.WtoMemberGroupId };

		public static readonly string[] GroupTypes = new[] { "A", "C", "F", "G", "N", "R", "S", "U", "X" };
	}
}