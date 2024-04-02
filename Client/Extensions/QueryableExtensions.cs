using System.Linq.Expressions;

namespace Client.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ConditionalWhere<T>(
         this IQueryable<T> query,
         bool shouldApplyFilter,
         Expression<Func<T, bool>> filterPredicate)
         => shouldApplyFilter == true
             ? query.Where(filterPredicate)
             : query;

        public static IEnumerable<T> ConditionalWhere<T>(
        this IEnumerable<T> query,
        bool shouldApplyFilter,
        Func<T, bool> filterPredicate)
        => shouldApplyFilter == true
            ? query.Where(filterPredicate)
            : query;
    }
}
