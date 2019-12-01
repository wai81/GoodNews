using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models.Categories
{
    public class GetCategoryByIdQueryModel : IRequest<Category>
    {
        public Guid Id { get; }
        public GetCategoryByIdQueryModel(Guid id)
        {
            Id = id;
        }
    }
}
