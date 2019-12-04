using System;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models.Categories
{
    public class GetCategoryByNameQueryModel : IRequest<Category>
    {
        public string Name { get; }
        public GetCategoryByNameQueryModel(string name)
        {
            Name = name;
        }

      
    }
}
