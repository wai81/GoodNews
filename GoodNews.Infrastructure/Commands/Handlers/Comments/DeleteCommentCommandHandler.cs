using System.Threading;
using System.Threading.Tasks;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Comments;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Handlers.Comments
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommandModel, bool>
    {

        private readonly ApplicationContext _context;
        public DeleteCommentCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCommentCommandModel request, CancellationToken cancellationToken)
        {
            var comment = _context.NewsComments.Find(request.Id);
            if (comment == null)
            {
                return false;
            }
            _context.NewsComments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
