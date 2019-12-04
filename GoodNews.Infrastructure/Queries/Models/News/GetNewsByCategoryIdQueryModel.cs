using System;
using System.Collections.Generic;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models
{
    public class GetNewsByCategoryIdQueryModel : IRequest<IEnumerable<DB.News>>
    {
        public Guid CategoryId { get;}

        public GetNewsByCategoryIdQueryModel(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
