using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Post;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Commands.Handlers.Post
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommandModel, bool>
    {
        private readonly ApplicationContext _context;

        public UpdateNewsCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateNewsCommandModel request, CancellationToken cancellationToken)
        {
            if (request.News != null)
            {
                try
                {
                    _context.News.UpdateRange(request.News);
                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                catch (Exception e)
                {
                    Log.Error($"Error while update news: {Environment.NewLine} {e.Message}");
                    return false;
                    throw;
                }
            }

            return false;
        }
    }
}
