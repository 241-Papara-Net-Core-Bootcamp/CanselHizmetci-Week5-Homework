using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CacheServiceAPI.Data.Abstracts
{
    public interface IRepository<T> where T:class
    {
        public IList<T> Get();
        public void Add(T entity, bool saveInstant = true);
        public void SaveChanges();
        //public void Remove(int id);
        //public void Update(int id, T entity);
    }
}
