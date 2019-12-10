using AutoMapper;

namespace Wto.Library.Core.AutoMapper {
    public interface IMapping {
        #region -- Interface Members --

        void CreateMappings(Profile configuration);

        #endregion
    }
}
