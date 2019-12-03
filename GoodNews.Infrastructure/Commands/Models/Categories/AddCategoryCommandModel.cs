using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Categories
{
    public class AddCategoryCommandModel : IRequest<bool>
    {
        public Category Category { get; }
        public AddCategoryCommandModel(Category category)
        {
            Category = category;
        }

    }
}
