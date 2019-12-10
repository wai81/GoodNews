using System.Collections.Generic;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Categories
{
    public class AddCategoryCommandModel : IRequest<bool>
    {
        public IEnumerable<Category> Category { get; }
        public AddCategoryCommandModel(IEnumerable<Category> category)
        {
            Category = category;
        }

    }
}
