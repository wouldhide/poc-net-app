using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wto.Library.Core.Attributes;
using Wto.Library.Core.Helpers;

namespace Wto.Library.Core.Application {
	public class SortOptionsProcessor<T> {
		#region -- Variable Declarations --

		private readonly string[] _orderBy;

		#endregion

		#region -- Constructor --

		public SortOptionsProcessor(string[] orderBy) {
			_orderBy = orderBy;
		}

		#endregion

		#region -- Functions --

		public IQueryable<T> Apply(IQueryable<T> query) {
			var terms = GetValidTerms().ToArray();

			if (!terms.Any()) {
				terms = GetTermsFromModel().Where(t => t.Default).ToArray();
			}

			if (!terms.Any()) return query;

			var modifiedQuery = query;
			var useThenBy = false;

			foreach (var term in terms) {
				var propertyInfo = ExpressionHelper
					.GetPropertyInfo<T>(term.Name);
				var obj = ExpressionHelper.Parameter<T>();

				// Build the LINQ expression backwards:
				// query = query.OrderBy(x => x.Property);

				// x => x.Property
				var key = ExpressionHelper
					.GetPropertyExpression(obj, propertyInfo);
				var keySelector = ExpressionHelper
					.GetLambda(typeof(T), propertyInfo.PropertyType, obj, key);

				// query.OrderBy/ThenBy[Descending](x => x.Property)
				modifiedQuery = ExpressionHelper
					.CallOrderByOrThenBy(
						modifiedQuery, useThenBy, term.Descending, propertyInfo.PropertyType, keySelector);

				useThenBy = true;
			}

			return modifiedQuery;
		}

		public IEnumerable<SortTerm> GetAllTerms() {
			if (_orderBy == null) yield break;

			foreach (var term in _orderBy) {
				if (string.IsNullOrEmpty(term)) continue;

				var tokens = term.Split(' ');

				if (tokens.Length == 0) {
					yield return new SortTerm { Name = term };
					continue;
				}

				var descending = tokens.Length > 1 && tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

				yield return new SortTerm {
					Name = tokens[0],
					Descending = descending
				};
			}
		}

		public IEnumerable<SortTerm> GetValidTerms() {
			var queryTerms = GetAllTerms().ToArray();
			if (!queryTerms.Any()) yield break;

			var declaredTerms = GetTermsFromModel();

			foreach (var term in queryTerms) {
				var declaredTerm = declaredTerms
					.SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
				if (declaredTerm == null) continue;

				yield return new SortTerm {
					Name = declaredTerm.Name,
					Descending = term.Descending,
					Default = declaredTerm.Default
				};
			}
		}

		private static IEnumerable<SortTerm> GetTermsFromModel()
			=> typeof(T).GetTypeInfo()
				.DeclaredProperties
				.Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
				.Select(p => new SortTerm {
					Name = p.Name,
					Descending = p.GetCustomAttribute<SortableAttribute>().DefaultDescending,
					Default = p.GetCustomAttribute<SortableAttribute>().Default
				});

		#endregion
	}
}