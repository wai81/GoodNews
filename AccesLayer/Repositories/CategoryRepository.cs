
using GoodNews.DB;


namespace DataAccesLayer.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
