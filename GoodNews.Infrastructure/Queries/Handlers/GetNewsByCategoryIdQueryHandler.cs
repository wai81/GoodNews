using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Queries.Handlers
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
            var result = _context.News.Where(c => c.CategoryID.Equals(request.CategoryId)).ToList();
            return result;
        }
    }
}
