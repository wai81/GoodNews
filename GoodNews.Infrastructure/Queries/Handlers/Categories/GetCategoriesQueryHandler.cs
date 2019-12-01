using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Categories
{
    class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryModel, IEnumerable<Category>>
    {
        private readonly ApplicationContext _context;

        public GetCategoriesQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> Handle(GetCategoriesQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.ToListAsync(cancellationToken: cancellationToken);
            return result;
        }
    }

}
