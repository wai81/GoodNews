using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.RatingCalculation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Queries.Handlers.RatingCalculation
{
    public class GetNewsIndexPositiveNullHandler : IRequestHandler<GetNewsIndexPositiveNullModel, IEnumerable<News>>
    {
        private readonly ApplicationContext _context;

        public GetNewsIndexPositiveNullHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<News>> Handle(GetNewsIndexPositiveNullModel request, CancellationToken cancellationToken)
        {
            var news = await _context.News.Where(n => n.IndexPositive == null).ToListAsync(cancellationToken);
            return news;
        }
    }
}
