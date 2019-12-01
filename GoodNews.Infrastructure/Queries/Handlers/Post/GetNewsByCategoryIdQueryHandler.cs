using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Post;
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
            var result = await _context.News.Where(c => c.CategoryID.Equals(request.CategoryId)).ToListAsync(cancellationToken);
            return result;
        }

    }
}
