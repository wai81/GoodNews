using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Categories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryModel, IEnumerable<Category>>
    {
        private readonly ApplicationContext _context;

        public GetCategoriesQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> Handle(GetCategoriesQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.OrderBy(c=>c.Name).ToListAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}
