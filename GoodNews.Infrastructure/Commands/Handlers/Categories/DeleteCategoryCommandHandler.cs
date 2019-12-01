using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Handlers.Categories
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
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
