namespace Wto.Library.Core.Application.Requests {
    public class BaseLocalizedListRequest : BaseLocalizedRequest {
	    #region -- Variable Declarations --

	    public const int DefaultItemsPerPage = 20;

	    #endregion

	    #region  Constructor --

	    public BaseLocalizedListRequest() {
		    this.Page = 1;
		    this.ItemsPerPage = DefaultItemsPerPage;
	    }

	    #endregion

        #region -- Properties --

        public int? Page { get; set; }

        public int? ItemsPerPage { get; set; }

		public string SortBy { get; set; }

        public WtoEnums.SortDirection? SortDirection { get; set; }

        public bool GetAll { get; set; }

        public string SearchTerm { get; set; }

        #endregion
    }
}