using System;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Comments
{
    public class AddCommentCommandModel : IRequest<Guid>
    {
        public NewsComment Comment { get; }
        public AddCommentCommandModel(NewsComment comment)
        {
            Comment = comment;
        }

    }
}
