using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Wto.Library.Core.Attributes;

namespace Wto.Library.Core.Entities {
    public class BaseEntity {
        #region -- Properties --

        /// <summary>
        /// Id of the entity. Primary Key.
        /// </summary>
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DbObjectDescription(Description = "Primary key/identier for the table.")]
        public int Id { get; set; }

        /// <summary>
        /// Created date. Date when the entity was inserted to the database.
        /// </summary>
        [IgnoreDataMember]
        [DbObjectDescription(Description = "Date when the record was created.")]
        public DateTime? CrDate { get; set; }

        /// <summary>
        /// Created by. User who inserted the entity to the database.
        /// </summary>
        [IgnoreDataMember]
        [DbObjectDescription(Description = "User who created the record.")]
        public string CrBy { get; set; }

        /// <summary>
        /// Modified date. Date when the entity was last updated in the database.
        /// </summary>
        [IgnoreDataMember]
        [DbObjectDescription(Description = "Date when the record was last modified.")]
        public DateTime? ModDate { get; set; }

        /// <summary>
        /// Modified by. User who last updated the entity in the database.
        /// </summary>
        [IgnoreDataMember]
        [DbObjectDescription(Description = "User who last modified the record.")]
        public string ModBy { get; set; }

        /// <summary>
        /// Used for concurrency check.
        /// </summary>
        [Timestamp, IgnoreDataMember]
        [DbObjectDescription(Description = "Row version to be used for concurrency check.")]
        public byte[] Timestamp { get; set; }

        #endregion
    }
}