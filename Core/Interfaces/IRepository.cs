using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
       

        IEnumerable<T> GetAll();
        IQueryable<T> GetAll_Q();
        T GetById(object id);
        void Create(T obj);
        void Update(T obj);
        void Delete(object id);

        IQueryable<T> AsQueryable();
        IQueryable<T> Include(string predicat);
        IEnumerable<T> Where(Func<T, bool> predicat);

        Task<T> GetByIdAsync(object id);
        Task<List<T>> GetAllAsync();

        IEnumerable<T> Find(Func<T, bool> predicat);
        Task CreateAsync(T obj);
        void AddRange(IEnumerable<T> objects);

       

        T CreateItem(T obj);
        
    }
}
