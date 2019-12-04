using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.DB;

namespace GoodNews.Infrastructure.Service.Parser
{
    public interface IParserSevice
    {
        IEnumerable<News> GetNewsFromUrl(string url);
    }
}
