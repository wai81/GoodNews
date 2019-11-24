using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Infrastructure.Commands.Models
{
    public class DeleteNewsCommandModel : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteNewsCommandModel(Guid id)
        {
            Id = id;
        }
    }
}
