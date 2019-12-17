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
        public int CountNewsOnPage { get; }
    public GetNewsPageQueryModel(int numberP = 1, int countNewsOnPage = 6)
        {
            NumberP = numberP;
            CountNewsOnPage = countNewsOnPage;
        }
        
    }
}
