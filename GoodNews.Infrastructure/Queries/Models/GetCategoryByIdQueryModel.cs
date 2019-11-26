using GoodNews.DB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Infrastructure.Queries.Models
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
