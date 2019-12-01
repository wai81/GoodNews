using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models.Post
{
    public class GetNewsByCategoryIdQueryModel : IRequest<IEnumerable<News>>
    {
        public Guid CategoryId { get; set; }

        public GetNewsByCategoryIdQueryModel(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
