using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Post
{
    public class GetNewsByCategoryIdQueryHandler : IRequestHandler<GetNewsByCategoryIdQueryModel, IEnumerable<News>>
    {
        private readonly ApplicationContext _context;

        public GetNewsByCategoryIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<News>> Handle(GetNewsByCategoryIdQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.News.Where(c => c.CategoryId.Equals(request.CategoryId)).ToListAsync(cancellationToken);
            return result;
        }
    }
}
