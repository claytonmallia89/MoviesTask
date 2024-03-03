using Movies.Common.Constants;

namespace Movies.Common
{
    public class QueryParameter
    {
        public string SearchQuery { get; set; } = string.Empty;
        public int? GenreId { get; set; }

        public int PageSize { get; set; } = DefaultConstants.DefaultPageSize;
        public int PageIndex { get; set; } = DefaultConstants.DefaultPageIndex;
    }
}