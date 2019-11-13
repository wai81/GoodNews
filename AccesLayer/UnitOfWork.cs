using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using GoodNews.DB;

namespace DataAccesLayer
{
    public class UnitOfWork : IUnitOfWork

    {
        private ApplicationContext _context;
        private IRepository<Category> _categoryRepository;
        private IRepository<News> _newsRepository;
        private IRepository<NewsComment> _newsCommentRepository;

        public UnitOfWork(ApplicationContext context,
            IRepository<Category> category,
            IRepository<News> news,
            IRepository<NewsComment> newsComment)
        {
            _context = context;
            _categoryRepository = category;
            _newsRepository = news;
            _newsCommentRepository = newsComment;
        }
        public IRepository<News> NewsRepository => _newsRepository;

        public IRepository<Category> CategoryRepository => _categoryRepository;

        public IRepository<NewsComment> NewsCommentRepository => _newsCommentRepository;



        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public Category GetCategorty(string nameCategory)
        {
            var _category = _categoryRepository.Find(c => c.Name.Equals(nameCategory)).FirstOrDefault();

            if (_category == null)
            {

                _category = new Category
                {
                    Name = nameCategory,
                   
                };

                _categoryRepository.Create(_category);
                Save();
            }

            return _category;

        }
    }
}
