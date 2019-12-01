using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Post
{
    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQueryModel, News>
    {
        private readonly ApplicationContext _context;

        public GetNewsByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<News> Handle(GetNewsByIdQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.News.FirstOrDefaultAsync(n => n.Id.Equals(request.Id), cancellationToken);

            return result;
        }

    }
}
