namespace Wto.Library.Core.Application.Requests {
    public class BaseLocalizedRequest {
        #region -- Constructor --

        public BaseLocalizedRequest() {
            this.Language = WtoEnums.Language.English;
        }

        #endregion

        #region -- Properties --

        public WtoEnums.Language Language { get; set; }

        #endregion
    }
}