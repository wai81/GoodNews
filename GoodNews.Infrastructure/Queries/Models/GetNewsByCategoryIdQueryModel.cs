using GoodNews.DB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Infrastructure.Queries.Models
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
