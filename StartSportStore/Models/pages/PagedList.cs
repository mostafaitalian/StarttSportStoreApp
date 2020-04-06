using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Diagnostics;
namespace StartSportStore.Models.pages
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IQueryable<T> query, QueryOption options = null)
        {
            PageSize = options.PageSize;
            CurrentPage = options.CurrentPage;
            Options = options;
            if (options != null) {
                if (!string.IsNullOrEmpty(options.SearchPropertyName) && !string.IsNullOrEmpty(options.SearchTerm))
                {
                    query = Search(query, options.SearchPropertyName, options.SearchTerm);
                }
                if (!string.IsNullOrEmpty(options.OrderPropertyName))
                {
                    query = Order(query, options.OrderPropertyName, options.DescendingOrder);
                }
            }
            Stopwatch sw = Stopwatch.StartNew();
            //Console.Clear();
            TotalPages = query.Count() / PageSize;
            AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
            Console.WriteLine($"the query time : {sw.ElapsedMilliseconds} ms");

        }


            
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public QueryOption Options { get; set; }
        public static IQueryable<T> Order(IQueryable<T> query, string PropertyName, bool desc)
        {
            var parameter = Expression.Parameter(typeof(T), "X");

            var source = PropertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
            //var body = Expression.Call()
            var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(T), source.Type), source, parameter);
            return typeof(Queryable).GetMethods().Single(
                method => method.Name == (desc ? "OrderByDescending": "OrderBy")
                && method.IsGenericMethodDefinition
                && method.GetGenericArguments().Length == 2
                && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), source.Type)
                .Invoke(null, new object[] { query, lambda }) as IQueryable<T>;
        }
        public IQueryable<T> Search(IQueryable<T> query, string PropertyName, string SearchTerm)
        {
            var Parameter = Expression.Parameter(typeof(T), "x");
            var source = PropertyName.Split('.').Aggregate((Expression)Parameter, Expression.Property);
            var body = Expression.Call(source, "Contains", Type.EmptyTypes, Expression.Constant(SearchTerm, typeof(string)));
            var lambda = Expression.Lambda<Func<T, bool>>(body, Parameter);
            return query.Where(lambda);
            //var Property = Expression.Property((Expression)Parameter, SearchTerm);
        }
    }
}
public class QueryOption
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string OrderPropertyName { get; set; }
    public bool DescendingOrder { get; set; }
    public string SearchPropertyName { get; set; }
    public string SearchTerm { get; set; }
}