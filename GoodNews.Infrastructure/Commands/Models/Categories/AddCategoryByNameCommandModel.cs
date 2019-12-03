using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.Categories
{
    public class AddCategoryByNameCommandModel : IRequest<Guid>
    {
        public AddCategoryByNameCommandModel(string name)
        {
            Name = name;
        }

       public Guid Id { get; set; }
       public string Name { get; set; }
    }
}
