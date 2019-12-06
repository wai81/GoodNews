using Core.Interfaces;
using GoodNews.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceParser.Parser
{
    public interface IParserSevice : IArticleServiceCQS
    {
        Task<IEnumerable<News>> ParserNewsFromUrl(string url);

    }
}
