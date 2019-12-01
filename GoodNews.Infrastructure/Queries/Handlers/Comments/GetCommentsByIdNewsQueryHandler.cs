using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Comments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Comments
{
    public class GetCommentsByIdNewsQueryHandler : IRequestHandler<GetCommentsByIdNewsQueryModel, IEnumerable<NewsComment>>
    {
        private readonly ApplicationContext _context;

        public GetCommentsByIdNewsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<NewsComment>> Handle(GetCommentsByIdNewsQueryModel request, CancellationToken cancellationToken)
        {
            var comments = await _context.NewsComments.Include("User").Include("News").ToListAsync(cancellationToken);
            var result = comments.Where(c => c.News.Id.Equals(request.Id)).ToList();
            return result;
        }

    }
}
