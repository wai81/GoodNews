using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Commands.Models.Post;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Handlers.Post
{
    public class AddNewsCommandHandler : IRequestHandler<AddNewsCommandModel, bool>
    {
        private readonly ApplicationContext _context;
        public AddNewsCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(AddNewsCommandModel request, CancellationToken cancellationToken)
        {
            await _context.News.AddAsync(request.News, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
