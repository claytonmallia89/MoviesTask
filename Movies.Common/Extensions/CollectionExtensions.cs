namespace Movies.Common.Extensions
{

    public  static class CollectionExtensions
    {
        /// <summary>
        /// This method is used for Pagination purposes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, int pageSize, int pageIndex)
        {
            return en.Skip(pageIndex * pageSize).Take(pageSize);
        }

        /// <summary>
        /// This method is used for Pagination purposes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> en, int pageSize, int pageIndex)
        {
            return en.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}
