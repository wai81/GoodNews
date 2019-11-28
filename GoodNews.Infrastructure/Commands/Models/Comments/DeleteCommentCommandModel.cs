using System;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Comments
{
    public class DeleteCommentCommandModel : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteCommentCommandModel(Guid id)
        {
            Id = id;
        }
    }
}
