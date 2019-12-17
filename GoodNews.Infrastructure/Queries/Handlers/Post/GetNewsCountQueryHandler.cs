using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Queries.Handlers.Post
{
    public class GetNewsCountQueryHandler : IRequestHandler<GetNewsCountQueryModel, long>
    {
        private readonly ApplicationContext _context;

        public GetNewsCountQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        

        public async Task<long> Handle(GetNewsCountQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.News.LongCountAsync(cancellationToken);
            return result;
        }
    }
}
