using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Wto.Library.Core.Extensions {
	public static class CollectionExtensions {
		#region -- Functions --

		public static Collection<T> ToCollection<T>(this IEnumerable<T> source) {
			var arr = source.ToArray();

			return new Collection<T>(arr);
		}

		public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> newValues) {
			var arr = newValues.ToArray();

			foreach (var value in arr) {
				source.Add(value);
			}
		}

		#endregion
	}
}
