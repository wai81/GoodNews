using GoodNews.DB;
using Models;

namespace DataAccesLayer.Repositories
{
    public class NewsCommentRepository : Repository<NewsComment>
    {
        public NewsCommentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
