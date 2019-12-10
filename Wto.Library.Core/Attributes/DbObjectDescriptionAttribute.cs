using System;

namespace Wto.Library.Core.Attributes {
    /// <summary>
    /// Defines the column description of properties to be saved in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbObjectDescriptionAttribute : Attribute {
        #region -- Variable Declarations --

        /// <summary>
        /// Description for the column.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region -- Constructor --

        /// <inheritdoc />
        public DbObjectDescriptionAttribute() {
			
        }

        /// <inheritdoc />
        public DbObjectDescriptionAttribute(string description) {
            this.Description = description;
        }

        #endregion
    }
}