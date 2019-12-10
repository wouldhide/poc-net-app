using System;

namespace Wto.Library.Core.AutoMapper {
    public sealed class Map {
        #region -- Properties --

        public Type Source { get; set; }
        public Type Destination { get; set; }

        #endregion
    }
}