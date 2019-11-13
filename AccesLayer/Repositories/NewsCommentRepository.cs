using GoodNews.DB;

namespace DataAccesLayer.Repositories
{
    public class NewsCommentRepository : Repository<NewsComment>
    {
        public NewsCommentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
