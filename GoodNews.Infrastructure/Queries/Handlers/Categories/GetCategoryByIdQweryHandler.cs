using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Categories
{
    public class GetCategoryByIdQweryHandler : IRequestHandler<GetCategoryByIdQueryModel, Category>
    {
        private readonly ApplicationContext _context;

        public GetCategoryByIdQweryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Category> Handle(GetCategoryByIdQueryModel request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(n => n.Id.Equals(request.Id), cancellationToken);

            return result;
        }
    }
}
