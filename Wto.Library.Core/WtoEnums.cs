using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wto.Library.Core {
    public class WtoEnums {
        #region -- Enums --

        public enum Language {
            English = 1,
            French = 2,
            Spanish = 3
        }

        public enum Domain {
            Tbt   = 1,
            Sps   = 2,
            Ag    = 3,
            Trips = 4,
            Gpa   = 5
        }

        public enum SortDirection {
            Asc,
            Desc
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum GroupTypes {
			A,
			C,
			F,
			G,
			N,
			R,
			S,
			U,
			X
        }

        #endregion
    }
}