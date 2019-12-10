using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.DB;

namespace Core
{
    public interface IParserSevice
    {
     
        IEnumerable<News> ParserNewsFromSource(string source);
       
        Task<IEnumerable<News>> ParserNewsFrom_S13(string url);
        Task<IEnumerable<News>> ParserNewsFrom_Onlainer(string url);
        Task<IEnumerable<News>> ParserNewsFrom_TUT(string url);

    }
}
