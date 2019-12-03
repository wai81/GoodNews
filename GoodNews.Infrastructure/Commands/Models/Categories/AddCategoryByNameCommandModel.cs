using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Categories
{
    public class AddCategoryByNameCommandModel : IRequest<Guid>
    {
        public Category Category { get; }
        public AddCategoryByNameCommandModel(Category category)
        {
            Category = category;
        }

    }
}
