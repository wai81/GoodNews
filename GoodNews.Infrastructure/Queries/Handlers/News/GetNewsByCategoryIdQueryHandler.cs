using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.News
{
    public class GetNewsByCategoryIdQueryHandler : IRequestHandler<GetNewsByCategoryIdQueryModel, IEnumerable<DB.News>>
    {
        private readonly ApplicationContext _context;

        public GetNewsByCategoryIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DB.News>> Handle(GetNewsByCategoryIdQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.News.Where(c => c.CategoryID.Equals(request.CategoryId)).ToListAsync(cancellationToken);
            return result;
        }
    }
}
