using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.News;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.News
{
    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQueryModel, DB.News>
    {
        private readonly ApplicationContext _context;

        public GetNewsByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<DB.News> Handle(GetNewsByIdQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.News.FirstOrDefaultAsync(n => n.Id.Equals(request.Id), cancellationToken);

            return result;
        }
    }
}
