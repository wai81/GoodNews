using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models.Comments
{
    public class GetCommentsByIdNewsQueryModel : IRequest<IEnumerable<NewsComment>>
    {
        public Guid Id { get; }
        public GetCommentsByIdNewsQueryModel(Guid id)
        {
            Id = id;
        }
    }
}
