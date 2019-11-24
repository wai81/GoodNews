using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Commands.Handlers
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
            await _context.News.AddAsync(request.News);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
