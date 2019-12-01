using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models.Post
{
    public class GetNewsByIdQueryModel : IRequest<News>
    {
        public Guid Id { get; }
        public Guid CategoryId { get; set; }

        public GetNewsByIdQueryModel(Guid id)
        {
            Id = id;
        }

        public GetNewsByIdQueryModel SetCategoryId(Guid categoryId)
        {
            CategoryId = categoryId;
            return this;
        }

    }
}
