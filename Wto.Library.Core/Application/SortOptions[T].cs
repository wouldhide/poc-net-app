using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Wto.Library.Core.Application {
	public class SortOptions<T> : IValidatableObject {
		#region -- Properties --

		public string[] OrderBy { get; set; }

		#endregion

		#region -- Functions --

		public IQueryable<T> Apply(IQueryable<T> query) {
			var processor = new SortOptionsProcessor<T>(OrderBy);
			return processor.Apply(query);
		}

		#endregion

		#region -- IValidatableObject Members --

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
			var processor = new SortOptionsProcessor<T>(OrderBy);

			var validTerms = processor.GetValidTerms().Select(x => x.Name);
			var invalidTerms = processor.GetAllTerms().Select(x => x.Name).Except(validTerms, StringComparer.OrdinalIgnoreCase);

			foreach (var term in invalidTerms) {
				yield return new ValidationResult(
					$"Invalid sort term '{term}'.",
					new[] { nameof(OrderBy) });
			}
		}

		#endregion
	}
}