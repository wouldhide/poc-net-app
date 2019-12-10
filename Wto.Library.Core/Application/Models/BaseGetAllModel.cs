namespace Wto.Library.Core.Application.Models {
    public class BaseGetAllModel<TModel> {
        #region -- Properties --

		public int Page { get; set; }
		public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }

        public TModel[] Items { get; set; }

        #endregion
    }
}