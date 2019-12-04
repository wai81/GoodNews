using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Categories
{
    public class AddCategoryByNameCommandModel : IRequest<Category>
    {
        public string Name { get; }
        public AddCategoryByNameCommandModel(string name)
        {
            Name = name;
        }

      
    }
}
