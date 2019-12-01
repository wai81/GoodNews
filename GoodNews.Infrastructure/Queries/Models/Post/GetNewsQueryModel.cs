using System.Collections.Generic;
using GoodNews.DB;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models.Post
{
    public class GetNewsQueryModel : IRequest<IEnumerable<News>>
    {
        
    }
}