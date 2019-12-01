using System;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Categories
{
    public class DeleteCategoryCommandModel : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteCategoryCommandModel(Guid id)
        {
            Id = id;
        }
    }
}
