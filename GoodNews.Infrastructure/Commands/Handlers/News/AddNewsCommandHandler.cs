using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Handlers.News
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoriCommandModel, bool>
    {
        private readonly ApplicationContext _context;
        public AddCategoryCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(AddCategoriCommandModel request, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(request.Category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
