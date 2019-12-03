using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GoodNews.Infrastructure.Commands.Handlers.Categories
{
    public class AddCategoryByNameCommandHandler : IRequestHandler<AddCategoryByNameCommandModel, Guid>
    {
        private readonly ApplicationContext _context;
        public AddCategoryByNameCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AddCategoryByNameCommandModel request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.Where(predicate: cn => cn.Name.Equals(request.Category.Name)).FirstOrDefaultAsync(cancellationToken);
            if (result == null)
            {
                _context.Categories.Add(request.Category);
                _context.SaveChanges();
                
            }
            result = await _context.Categories.Where(predicate: cn => cn.Name.Equals(request.Category.Name)).FirstOrDefaultAsync(cancellationToken);
            return result.Id;
        }
    }
}
