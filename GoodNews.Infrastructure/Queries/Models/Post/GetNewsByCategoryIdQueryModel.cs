using System;
using System.Collections.Generic;
using MediatR;
using GoodNews.DB;

namespace GoodNews.Infrastructure.Queries.Models
{
    public class GetNewsByCategoryIdQueryModel : IRequest<IEnumerable<News>>
    {
        public Guid CategoryId { get;}

        public GetNewsByCategoryIdQueryModel(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
