using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Handlers.Post
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandModel, bool>
    {
        private readonly ApplicationContext _context;
        public DeleteCategoryCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommandModel request, CancellationToken cancellationToken)
        {
            var news = await _context.News.FindAsync(request.Id);
            if (news == null)
            {
                return false;
            }
            _context.News.Remove(news);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
