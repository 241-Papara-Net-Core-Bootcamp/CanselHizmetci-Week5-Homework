using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CacheServiceAPI.Data.Abstracts;
using CacheServiceAPI.Data.Context;
using CacheServiceAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CacheServiceAPI.Data.Concretes
{
    public class Repository<T> : IRepository<T> where T :BaseEntity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IList<T> Get()
        {
           return _context.Set<T>().ToList();
        }

        public void Add(T entity, bool saveInstant=true)
        {
            _context.Set<T>().Add(entity);
            if (saveInstant)
                _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        //public void Remove(int id)
        //{
        //    var e = _context.Set<T>().Find(id);
        //    if (e == null) return;
        //    _context.Entry(e).State = EntityState.Deleted;
        //    _context.SaveChanges();

        //}

        //public void Update(int id, T entity)
        //{
        //    var e = _context.Set<T>().Find(id);
        //    if (e == null) return;
        //    entity.Id = id;
        //    _context.Entry(e).CurrentValues.SetValues(entity);
        //    _context.SaveChanges();
        //}
    }
}
