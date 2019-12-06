using System.Collections.Generic;
using MediatR;
using GoodNews.DB;

namespace GoodNews.Infrastructure.Queries.Models
{
    public class GetNewsQueryModel : IRequest<IEnumerable<News>>
    {
       
    }
}
