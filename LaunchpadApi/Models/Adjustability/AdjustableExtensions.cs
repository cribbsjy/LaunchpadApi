using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace Launchpad.Api.Models.Adjustability
{
    /// <summary>
    /// Extension methods to perform sorting, paging, filter, and field selection operations
    /// off the <see cref="IAdjustable"/> interface
    /// </summary>
    public static class AdjustableExtensions
    {
        /// <summary>
        /// Returns a page of data based on the <see cref="IAdjustable.Skip"/>
        /// and <see cref="IAdjustable.Take"/> properties
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, IAdjustable adj)
        {
            return Paginate(source.AsQueryable(), adj).AsEnumerable();
        }

        /// <summary>
        /// Returns a page of data based on the <see cref="IAdjustable.Skip"/>
        /// and <see cref="IAdjustable.Take"/> properties
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, IAdjustable adj)
        {
            var data = source;
            if (adj.Skip.HasValue)
            {
                data = data.Skip(adj.Skip.Value);
            }

            if (adj.Take.HasValue)
            {
                data = data.Take(adj.Take.Value);
            }

            return data;
        }

        /// <summary>
        /// Returns a list of objects that only contains the properties from the
        /// <see cref="IAdjustable.Fields"/> column
        /// properties are specified, all the properties are returned
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        public static IEnumerable<ExpandoObject> FieldSelect<T>(this IEnumerable<T> source, IAdjustable adj)
        {
            return source.Select(s => s.FieldSelect(adj?.Fields));
        }

        /// <summary>
        /// Receives a single object and performs a fields selection operation 
        /// with the given <see cref="fields"/>
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        public static ExpandoObject FieldSelect<T>(this T obj, string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return ToExpando(obj);
            }

            var columns = fields.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToList();
            if (columns.All(string.IsNullOrWhiteSpace))
            {
                return ToExpando(obj);
            }

            var expando = new ExpandoObject();
            var dict = (IDictionary<string, object>)expando;

            foreach (var column in columns)
            {
                var property = typeof(T)
                    .GetProperties()
                    .FirstOrDefault(p => p.Name.ToLower() == column?.ToLower());
                if (property != null)
                {
                    dict[property.Name] = property.GetValue(obj);
                }
            }

            return expando;
        }

        /// <summary>
        /// Orders the given collection based on the <see cref="IAdjustable.Sort"/> property
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, IAdjustable adj)
        {
            if (string.IsNullOrWhiteSpace(adj?.Sort))
            {
                return source;
            }

            var columns = adj.Sort
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToList();

            bool thenBy = false;
            foreach (var column in columns)
            {
                source = SortByParam(source, column, thenBy);
                thenBy = true;
            }

            return source;
        }

        /// <summary>
        /// Orders the given collection based on the <see cref="IAdjustable.Sort"/> property
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        public static IEnumerable<T> SortBy<T>(this IEnumerable<T> source, IAdjustable adj)
        {
            return SortBy(source.AsQueryable(), adj).AsEnumerable();
        }

        private static IQueryable<T> SortByParam<T>(IQueryable<T> query, string column, bool thenBy)
        {
            bool desc = column.StartsWith("-");

            column = column.Trim('-').Trim('+')?.ToLower();

            var hasproperty = typeof(T).GetProperties().Any(p => p.Name.ToLower() == column);

            if (!hasproperty)
            {
                return query;
            }

            string orderby;
            if (thenBy)
            {
                orderby = desc
                    ? nameof(Enumerable.ThenByDescending)
                    : nameof(Enumerable.ThenBy);
            }
            else
            {
                orderby = desc
                    ? nameof(Enumerable.OrderByDescending)
                    : nameof(Enumerable.OrderBy);
            }

            // https://stackoverflow.com/questions/34899933/sorting-using-property-name-as-string
            // LAMBDA: x => x.[PropertyName]
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression property = Expression.Property(parameter, column);
            var lambda = Expression.Lambda(property, parameter);

            // REFLECTION: source.OrderBy(x => x.Property)
            var orderByMethod = typeof(Queryable).GetMethods().First(x => x.Name == orderby && x.GetParameters().Length == 2);
            var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(T), property.Type);
            var result = orderByGeneric.Invoke(null, new object[] { query, lambda });

            return (IQueryable<T>)result;
        }

        private static ExpandoObject ToExpando<T>(this T obj)
        {
            var expando = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)expando;

            foreach (var property in obj.GetType().GetProperties())
            {
                dictionary.Add(property.Name, property.GetValue(obj));
            }

            return expando;
        }
    }
}
