using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GoodNews.Infrastructure.Commands.Handlers.Categories
{
    public class AddCategoryByNameCommandHandler : IRequestHandler<AddCategoryByNameCommandModel, Category>
    {
        private readonly ApplicationContext _context;
       
        public AddCategoryByNameCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Category> Handle(AddCategoryByNameCommandModel request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.ToListAsync(cancellationToken: cancellationToken);
            var result = categories.Where(c => c.Name.Equals(request.Name)).FirstOrDefault();
            //var result = await mediator.Send(new GetCategoryByNameQueryModel(request.Name), cancellationToken);
            if (result == null)
            {
                result = new Category { Name = request.Name };
                _context.Add(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }
    }
}
