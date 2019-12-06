using System;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Post
{
    public class DeleteNewsCommandModel : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteNewsCommandModel(Guid id)
        {
            Id = id;
        }
    }
}
