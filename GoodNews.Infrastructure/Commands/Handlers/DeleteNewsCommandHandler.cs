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
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommandModel, bool>
    {

        private readonly ApplicationContext _context;
        public DeleteNewsCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteNewsCommandModel request, CancellationToken cancellationToken)
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
