using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Comments;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Handlers.Comments
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommandModel, Guid>
    {
        private readonly ApplicationContext _context;
        public AddCommentCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(AddCommentCommandModel request, CancellationToken cancellationToken)
        {
            await _context.NewsComments.AddAsync(request.Comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Comment.Id;
        }
    }
}
