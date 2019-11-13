using System;
using System.Threading.Tasks;
using GoodNews.DB;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<News> NewsRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<NewsComment> NewsCommentRepository { get; }
        Task<int> SaveAsync();
        void Save();
       Category GetCategorty(string name);
    }
}
