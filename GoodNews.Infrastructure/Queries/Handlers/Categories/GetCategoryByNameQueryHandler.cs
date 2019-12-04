using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Categories
{
    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQueryModel, Category>
    {
        private readonly ApplicationContext _context;

        public GetCategoryByNameQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public Task<Category> Handle(GetCategoryByNameQueryModel request, CancellationToken cancellationToken)
        {
            var result =  _context.Categories.Where(c=>c.Name.Equals(request.Name)).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return result;
        }
    }
}
