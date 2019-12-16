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
    public class GetNewsQueryHandler : IRequestHandler<GetNewsQueryModel, IEnumerable<News>>
    {
        private readonly ApplicationContext _context;

        public GetNewsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> Handle(GetNewsQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.News.OrderBy(n=>n.DateCreate).ToListAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}
