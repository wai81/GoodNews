using Core.Interfaces;
using GoodNews.DB;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace ServiceParser.Parser
{
    public interface IParserSevice : IArticleServiceCQS
    {
     
        IEnumerable<News> ParserNewsFromSource(string source);
       
        Task<IEnumerable<News>> ParserNewsFrom_S13(string url);
        Task<IEnumerable<News>> ParserNewsFrom_Onlainer(string url);
        Task<IEnumerable<News>> ParserNewsFrom_TUT(string url);

    }
}
