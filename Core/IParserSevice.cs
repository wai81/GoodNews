using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.DB;

namespace Core
{
    public interface IParserSevice
    {
     
        IEnumerable<News> ParserNewsFromSource(string source);
       
    }
}
