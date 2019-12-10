using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Wto.Library.Core.Attributes;

namespace Wto.Library.Core.Entities {
    public class BaseLanguageEntity : BaseEntity {
        #region -- Properties --

        /// <summary>
        /// Id of the entity. Part of the composite Primary Key.
        /// </summary>
        [Key, Required, Column(Order = 1)]
        [DbObjectDescription(Description = "Primary key/identier for the table.")]
        public new int Id { get; set; }

        /// <summary>
        /// Language for the record.
        /// </summary>
        [Key, Required, Column(Order = 2)]
        [DbObjectDescription(Description = "Language for the record.")]
        public WtoEnums.Language Language { get; set; }

        [NotMapped, IgnoreDataMember]
        public new DateTime? CrDate { get; set; }

        [NotMapped, IgnoreDataMember]
        public new string CrBy { get; set; }

        [NotMapped, IgnoreDataMember]
        public new DateTime? ModDate { get; set; }

        [NotMapped, IgnoreDataMember]
        public new string ModBy { get; set; }

        [NotMapped, IgnoreDataMember]
        public new byte[] Timestamp { get; set; }

        #endregion
    }
}