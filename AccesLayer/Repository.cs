using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using GoodNews.DB;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccesLayer
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private ApplicationContext _context;
        private DbSet<T> _table;

        public Repository(ApplicationContext _context)
        {
            this._context = _context;
            _table = _context.Set<T>();
        }
        public void Create(T obj)
        {
            _table.Add(obj);
        }

        public void Delete(object id)
        {
            T exist = _table.Find(id);
            _table.Remove(exist);
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public IQueryable<T> AsQueryable()
        { 
            return _table.AsQueryable();
        }

        public int Count()
        {
            return _table.Count();
        }

        public IQueryable<T> GetAll_Q()
        {
            return _table;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }
        public IEnumerable<T> Find(Func<T, bool> predicat)
        {
            return _table.Where(predicat);
        }

        public async Task CreateAsync(T obj)
        {
            await _table.AddAsync(obj);
        }

        public T CreateItem(T obj)
        {
            _table.Add(obj);
            return _table.Find(obj);
        }
        public void AddRange(IEnumerable<T> objects)
        {
            _table.AddRange(objects);
        }
        public IQueryable<T> Include(string predicat)
        {
            return _table.Include(predicat);
        }
        public IEnumerable<T> Where(Func<T, bool> predicat)
        {
            return _table.Where(predicat);
        }

    }
}
