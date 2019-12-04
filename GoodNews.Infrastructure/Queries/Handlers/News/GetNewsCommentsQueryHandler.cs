using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.News;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.News
{
    class GetNewsCommentsQueryHandler : IRequestHandler<GetNewsCommentsQueryModel, IEnumerable<NewsComment>>
    {
        private readonly ApplicationContext _context;

        public GetNewsCommentsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<NewsComment>> Handle(GetNewsCommentsQueryModel request, CancellationToken cancellationToken)
        {
            var comments = await _context.NewsComments.Include("User").Include("News").ToListAsync(cancellationToken);
            var result = comments.Where(c => c.News.Id.Equals(request.Id)).ToList();
            return result;
        }
    }
}
