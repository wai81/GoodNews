using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.DB;

namespace GoodNews.Infrastructure.Service.Parser
{
    public interface IParserSevice
    {
        Task<IEnumerable<News>> GetNewsFromUrlAsync(string url);
    }
}
