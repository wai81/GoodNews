using GoodNews.DB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Infrastructure.Queries.Models.Post
{
    public class GetNewsPageQueryModel : IRequest<IEnumerable<News>>
    {
        public int NumberP { get; }
        
    public GetNewsPageQueryModel(int numberP)
        {
            NumberP = numberP;
        }
        
    }
}
