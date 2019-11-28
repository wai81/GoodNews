using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Categories
{
    public class AddCategoriCommandModel : IRequest<bool>
    {
        public Category Category { get; }
        public AddCategoriCommandModel(Category category)
        {
            Category = category;
        }

    }
}
