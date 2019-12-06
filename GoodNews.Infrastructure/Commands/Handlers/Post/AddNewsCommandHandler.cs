using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Post;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Handlers.Post
{
    public class AddCategoryCommandHandler : IRequestHandler<AddNewsCommandModel, bool>
    {
        private readonly ApplicationContext _context;
        public AddCategoryCommandHandler(ApplicationContext context)
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
