using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Queries.Handlers
{
    public class GetNewsByIdQweryHandler : IRequestHandler<GetNewsByIdQueryModel, News>
    {
        private readonly ApplicationContext _context;

        public GetNewsByIdQweryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<News> Handle(GetNewsByIdQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.News.FirstOrDefaultAsync(n => n.Id.Equals(request.Id));

            return result;
        }
    }
}
