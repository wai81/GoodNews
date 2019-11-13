
using GoodNews.DB;

namespace DataAccesLayer.Repositories
{
    public class NewsRepository : Repository<News>
    {
        public NewsRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
