
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        ValueTask<(IEnumerable<T> data, int pageNumber, int pagesize, int count)> GetAll(string filter = null, string orderby = null, string includeProperties = null, int pageNumber = 1, int pageSize = 10);

        Task<T> GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string inludeProperties = null
            );

        Task Add(T entity);
        Task Remove(T entity);
        Task Remove(int id);
        Task Remove(Guid id);
        void Update(T entity, T origentity);


    }
}
