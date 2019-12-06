using System;
using System.Collections.Generic;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models
{
    public class GetNewsCommentsQueryModel : IRequest<IEnumerable<NewsComment>>
    {
        public GetNewsCommentsQueryModel(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }

    }
}
