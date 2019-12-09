using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Post
{
    public class GetNewsByUrlExistHandler : IRequestHandler<GetNewsByUrlExistModel, bool>
    {
        private readonly ApplicationContext _context;

        public GetNewsByUrlExistHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(GetNewsByUrlExistModel request, CancellationToken cancellationToken)
        {
            bool result = _context.News.Any(n=>n.LinkURL.Equals(request.Url));
            return await Task.FromResult(result);
        }
    }
}
