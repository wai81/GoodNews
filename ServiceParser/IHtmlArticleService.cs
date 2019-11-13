using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using GoodNews.DB;

namespace ServiceParser
{
    public interface IHtmlArticleService : IArticleService
    {
        IEnumerable<News> GetArticlesFrom_Onlainer(string url);
        IEnumerable<News> GetArticlesFrom_TUT(string url);
        IEnumerable<News> GetArticlesFrom_S13(string url);
       
        bool AddRange(IEnumerable<News> articles);
        Task<bool> AddRangeAsync(IEnumerable<News> objects);

    }
}
