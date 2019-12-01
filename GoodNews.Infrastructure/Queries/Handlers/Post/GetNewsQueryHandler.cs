using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Post;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            var result = await _context.News.ToListAsync(cancellationToken);
            return result;
        }

    }
}
