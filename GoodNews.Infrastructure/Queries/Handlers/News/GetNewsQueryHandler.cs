using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.News
{
    public class GetNewsQueryHandler : IRequestHandler<GetNewsQueryModel, IEnumerable<DB.News>>
    {
        private readonly ApplicationContext _context;

        public GetNewsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DB.News>> Handle(GetNewsQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.News.ToListAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}
