using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Service.Parser
{
    public interface INewsFromUrl
    {
        Task<bool> GetNewsUrl(string url);
    }
}