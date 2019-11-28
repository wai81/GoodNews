using System.Collections.Generic;
using MediatR;

namespace GoodNews.Infrastructure.Queries.Models
{
    public class GetNewsQueryModel : IRequest<IEnumerable<DB.News>>
    {
       
    }
}
