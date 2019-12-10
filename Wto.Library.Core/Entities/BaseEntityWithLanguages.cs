using System.Collections.Generic;

namespace Wto.Library.Core.Entities {
    public class BaseEntityWithLanguages<TLanguages> : BaseEntity {
        #region -- Properties --

        public virtual ICollection<TLanguages> Languages { get; set; }

        #endregion
    }
}