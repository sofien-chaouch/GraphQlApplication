using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GraphQlApplication.Data;
using GraphQlApplication.Interfaces;

namespace GraphQlApplication.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _context;
        private DbSet<T> table;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            T exists = table.Find(id);
            table.Remove(exists);
            _context.Entry(exists).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void DeleteObject(T obj)
        {
            table.Remove(obj);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<T> GetWithInclude(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table.AsNoTracking();

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.Count() > 0 ? query.ToList() : null;
        }
    }
}
