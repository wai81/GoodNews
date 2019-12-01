using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.Infrastructure.Queries.Handlers.Categories
{
    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQueryModel, Category>
    {
        private readonly ApplicationContext _context;
        public GetCategoryByNameQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Category> Handle(GetCategoryByNameQueryModel request, CancellationToken cancellationToken)
        {
           var result = await _context.Categories.Where(c => c.News.Equals(request.Name)).FirstOrDefaultAsync(cancellationToken);
           return result;
        }
    }
}
