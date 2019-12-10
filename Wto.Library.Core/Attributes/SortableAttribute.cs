using System;

namespace Wto.Library.Core.Attributes {
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class SortableAttribute : Attribute {
		#region -- Properties --

		public bool Default { get; set; }

		public bool DefaultDescending { get; set; }

		#endregion
	}
}