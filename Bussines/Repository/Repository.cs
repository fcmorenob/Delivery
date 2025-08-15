using Bussines.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Bussines.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DeliveryContext context;

        internal DbSet<T> dbSet;
        public Repository(DeliveryContext _context)
        {
            context = _context;
            this.dbSet = context.Set<T>();

        }
        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async ValueTask<(IEnumerable<T> data, int pageNumber, int pagesize, int count)> GetAll(string filter = null, string orderby = null, string includeProperties = null, int pageNumber = 1, int pageSize = 10)
        {
            Expression<Func<T, bool>> filterExpress = filter != null ? BuildFilterExpression(filter) : null;
            Func<IQueryable<T>, IOrderedQueryable<T>> orderbyExpress = orderby != null ? BuildOrderByExpression(orderby) : null;
            IQueryable<T> query = dbSet;
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;


            if (filterExpress != null)
            {
                query = query.Where(filterExpress);
            }
            int totalCount = await query.CountAsync();

            if (includeProperties != null)
            {
                foreach (var propertie in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertie.Trim());
                }
            }
            if (orderbyExpress != null)
            {
                query = orderbyExpress(query);
            }
            if ((pageNumber >= 1) && (pageSize >= 10))
            { //query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            var retorno = await query.ToListAsync();

            return (retorno, pageNumber, pageSize, totalCount);
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            //includeProperties = "Departamentos";

            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var propertie in includeProperties.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertie);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task Remove(int id)
        {
            var rem = await dbSet.FindAsync(id);
            dbSet.Remove(rem);
        }

        public async Task Remove(List<int> ids)
        {
            List<T> entity = new List<T>();
            foreach (int id in ids)
            {
                entity.Add(await dbSet.FindAsync(id));
            }

            dbSet.RemoveRange(entity);
        }
        public async Task Remove(Guid id)
        {
            var rem = await dbSet.FindAsync(id);
            dbSet.Remove(rem);
        }

        public void Update(T entity, T origentity)
        {
            dbSet.Attach(origentity);
            var entry = dbSet.Entry(origentity);

            foreach (var propertyEntry in entry.Properties)
            {
                var propName = propertyEntry.Metadata.Name;
                var propInfo = entity.GetType().GetProperty(propName);
                if (propInfo == null) continue;

                var newValue = propInfo.GetValue(entity);
                if ((propInfo.PropertyType == typeof(int) || propInfo.PropertyType == typeof(long) || propInfo.PropertyType == typeof(short)) && Convert.ToInt64(newValue) == 0)
                {
                    continue;
                }
                if (newValue != null && !Equals(propertyEntry.CurrentValue, newValue))
                {
                    propertyEntry.CurrentValue = newValue;
                }
            }
        }

        #region FilterConversors
        private Expression<Func<T, bool>> BuildFilterExpression(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return null;

            var parameter = Expression.Parameter(typeof(T), "entity");
            Expression combinedExpression = null;

            var filters = filter.Split(new[] { " AND ", " OR " }, StringSplitOptions.None);
            var operators = new List<string>();

            int lastIndex = 0;
            foreach (var match in System.Text.RegularExpressions.Regex.Matches(filter, " AND | OR "))
            {
                operators.Add(match.ToString().Trim());
            }

            for (int i = 0; i < filters.Length; i++)
            {
                var parts = filters[i].Split(new[] { ' ' }, 3);
                if (parts.Length != 3)
                    throw new ArgumentException("Formato de filtro no válido. Use 'PropertyName Operator PropertyValue'", nameof(filter));

                var propertyName = parts[0].Trim();
                var operatorType = parts[1].Trim().ToUpper();
                var propertyValue = parts[2].Trim();

                var property = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (property == null)
                    throw new ArgumentException($"Propiedad '{propertyName}' no encontrada en {typeof(T)}", nameof(filter));
                Expression constant = null;
                object propertyValueTyped;
                if (property.PropertyType == typeof(Guid))
                {
                    propertyValueTyped = new Guid(propertyValue);
                    constant = Expression.Constant(propertyValueTyped);
                }
                else if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
                    propertyValueTyped = Convert.ChangeType(propertyValue, underlyingType);
                    constant = Expression.Constant(propertyValueTyped, underlyingType);
                }
                else
                {
                    propertyValueTyped = Convert.ChangeType(propertyValue, property.PropertyType);
                    constant = Expression.Constant(propertyValueTyped);
                }

                var propertyAccess = Expression.Property(parameter, property);
                var nonNullableAccess = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))
                    ? Expression.Property(propertyAccess, "Value")
                    : propertyAccess;

                Expression comparison;

                switch (operatorType)
                {
                    case "=":
                        comparison = Expression.Equal(propertyAccess, constant);
                        break;

                    case "!=":
                        comparison = Expression.NotEqual(propertyAccess, constant);
                        break;

                    case "<":
                        comparison = Expression.LessThan(propertyAccess, constant);
                        break;

                    case "<=":
                        comparison = Expression.LessThanOrEqual(propertyAccess, constant);
                        break;

                    case ">":
                        comparison = Expression.GreaterThan(propertyAccess, constant);
                        break;

                    case ">=":
                        comparison = Expression.GreaterThanOrEqual(propertyAccess, constant);
                        break;

                    case "LK":
                        
                        comparison = Expression.Call(
                            propertyAccess,
                            typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                            constant
                        );
                        break;

                    default:
                        throw new ArgumentException($"Operador '{operatorType}' no soportado.", nameof(operatorType));
                }

                // Combinar expresiones
                if (combinedExpression == null)
                {
                    combinedExpression = comparison;
                }
                else
                {
                    var logicalOperator = operators[i - 1];
                    if (logicalOperator == "AND")
                        combinedExpression = Expression.AndAlso(combinedExpression, comparison);
                    else if (logicalOperator == "OR")
                        combinedExpression = Expression.OrElse(combinedExpression, comparison);

                }
            }

            return Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        }

        private Func<IQueryable<T>, IOrderedQueryable<T>> BuildOrderByExpression(string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return null;

            var parts = orderBy.Split(',');
            var parameter = Expression.Parameter(typeof(T), "entity");
            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc = queryable =>
            {
                IOrderedQueryable<T> orderedQuery = null;
                foreach (var part in parts)
                {
                    var propertyName = part.Trim();
                    var property = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                    if (property == null)
                        throw new ArgumentException($"Propiedad '{propertyName}' no encontrada en {typeof(T)}", nameof(orderBy));

                    var propertyAccess = Expression.Property(parameter, property);
                    var orderByExpression = Expression.Lambda(propertyAccess, parameter);

                    if (orderedQuery == null)
                    {
                        orderedQuery = Queryable.OrderBy(queryable, (dynamic)orderByExpression);
                    }
                    else
                    {
                        orderedQuery = Queryable.ThenBy(orderedQuery, (dynamic)orderByExpression);
                    }
                }
                return orderedQuery;
            };

            return orderFunc;
        }
        #endregion
    }
}