﻿using GoodNews.DB;
using GoodNews.Infrastructure.Queries.Models.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoodNews.Infrastructure.Queries.Handlers.Post
{
    public class GetNewsPageQueryHandler : IRequestHandler<GetNewsPageQueryModel, IEnumerable<News>>
    {
        private readonly ApplicationContext _context;

        public GetNewsPageQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> Handle(GetNewsPageQueryModel request, CancellationToken cancellationToken)
        {
            int countNewsOnPage = 10;
                var result = await _context.News.OrderByDescending(n => n.DateCreate).OrderByDescending(p => p.IndexPositive).Skip((request.NumberP - 1) * countNewsOnPage)
                 //var result = await _context.News.OrderByDescending(n=>n.IndexPositive).Skip((request.NumberP - 1) * countNewsOnPage)
                 .Take(countNewsOnPage)
                 .ToListAsync(cancellationToken);

            return result;
        }
    }
}
