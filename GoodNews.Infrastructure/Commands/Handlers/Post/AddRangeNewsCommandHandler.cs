using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Post;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Handlers.Post
{
    public class AddRangeNewsCommandHandler : IRequestHandler<AddRangeNewsCommandModel, bool>
    {
        private readonly ApplicationContext _context;
        public AddRangeNewsCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(AddRangeNewsCommandModel request, CancellationToken cancellationToken)
        {
            await _context.News.AddRangeAsync(request.News, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
