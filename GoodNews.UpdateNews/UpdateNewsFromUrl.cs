using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Commands.Models.Post;
using GoodNews.Infrastructure.Queries.Handlers.Categories;
using GoodNews.Infrastructure.Queries.Models;
using GoodNews.Infrastructure.Queries.Models.Categories;
using MediatR;
using ServiceParser.Parser;

namespace GoodNews.UpdateNewsServices
{
    public class NewsService : INewsService
    {
        private readonly IMediator _mediator;
        private readonly IParserSevice _parser;
       

        public NewsService(IMediator mediator, IParserSevice parserSevice)
        {
            _mediator = mediator;
            _parser = parserSevice;
        }

        

        public async Task<bool> RequestUpdateNewsFromSourse(string sorseURL)
        {
            //var categoryNews = new Dictionary<Guid,Category>();
            List<Category> categoryName = new List<Category>();
            var dataSourse = _parser.ParserNewsFromSource(sorseURL);
            var newsAll = await _mediator.Send(new GetNewsQueryModel());
            foreach (var cat in dataSourse)
            {
                if (await _mediator.Send(new GetCategoryByNameQueryModel(cat.CategoryName)) == null)
                {
                    categoryName.Add(new Category() { Name = cat.CategoryName });
                }
            }

            if (await _mediator.Send(new AddCategoryCommandModel(categoryName)))
            {}

            //var allCategory = await _mediator.Send(new GetCategoriesQueryModel());
            foreach (var news in dataSourse)
            {
                if (newsAll.Count(c => c.LinkURL.Equals(news.LinkURL)) == 0)
                {
                    news.Category = await _mediator.Send(new GetCategoryByNameQueryModel(news.CategoryName));
                }
            }

            //await _mediator.Send(new AddNewsCommandModel(news));
            return true;
        }


        //public async Task<bool> ParserNewsTUT()
        //{
        //    IEnumerable<News> newsTut = new List<News>();
        //    var newsAll = await _mediator.Send(new GetNewsQueryModel());
        //    newsTut = await _parser.ParserNewsFrom_TUT(@"https://news.tut.by/rss/all.rss");
        //    foreach (var item in newsTut)
        //    {
        //        if (newsAll.Count(c => c.LinkURL.Equals(item.LinkURL)) == 0)
        //        {
        //            await _mediator.Send(new AddNewsCommandModel(item));
        //        }
        //    }
        //    return true;
        //}
        //public async Task<bool> ParserNewsS13()
        //{
        //    IEnumerable<News> newsS13 = new List<News>();
        //    var newsAll = await _mediator.Send(new GetNewsQueryModel());
        //    newsS13 = await _parser.ParserNewsFrom_S13(@"http://s13.ru/rss");
        //    foreach (var s in newsS13)
        //    {
        //        if (newsAll.Count(c => c.LinkURL.Equals(s.LinkURL)) == 0)
        //        {
        //            await _mediator.Send(new AddNewsCommandModel(s));
        //        }
        //    }
        //    return true;
        //}
        //public async Task<bool> ParserNewsOnlainer()
        //{
        //   // IEnumerable<News> newsOnl = new List<News>();
        //    var newsAll = await _mediator.Send(new GetNewsQueryModel());
        //    var newsOnl = await _parser.ParserNewsFrom_Onlainer(@"https://people.onliner.by/feed");
        //    foreach (var o in newsOnl)
        //    {
        //        if (newsAll.Count(c => c.LinkURL.Equals(o.LinkURL)) == 0)
        //        {
        //            await _mediator.Send(new AddNewsCommandModel(o));
        //        }
        //    }
        //    return true;
        //}



        //public async Task<bool> ParserNewsByUrl()
        //{
        //    IEnumerable<News> newsTut = new List<News>();
        //    IEnumerable<News> newsS13 = new List<News>();
        //    IEnumerable<News> newsOnl = new List<News>();
        //    var newsAll = await _mediator.Send(new GetNewsQueryModel());

        //    //newsOnl = await _parser.ParserNewsFrom_Onlainer(@"https://people.onliner.by/feed");
        //    //foreach (var o in newsOnl)
        //    //{
        //    //    if (newsAll.Count(c => c.LinkURL.Equals(o.LinkURL)) == 0)
        //    //    {
        //    //        await _mediator.Send(new AddNewsCommandModel(o));
        //    //    }
        //    //}

        //    //newsTut = await _parser.ParserNewsFrom_TUT(@"https://news.tut.by/rss/all.rss");
        //    //foreach (var item in newsTut)
        //    //{
        //    //    if (newsAll.Count(c => c.LinkURL.Equals(item.LinkURL)) == 0)
        //    //    {
        //    //        await _mediator.Send(new AddNewsCommandModel(item));
        //    //    }
        //    //}

        //    //newsS13 = await _parser.ParserNewsFrom_S13(@"http://s13.ru/rss");
        //    //foreach (var s in newsS13)
        //    //{
        //    //    if (newsAll.Count(c => c.LinkURL.Equals(s.LinkURL)) == 0)
        //    //    {
        //    //        await _mediator.Send(new AddNewsCommandModel(s));
        //    //    }
        //    //}

        //    Parallel.Invoke(
        //         async () =>
        //         {
        //             newsTut = await _parser.ParserNewsFrom_TUT(@"https://news.tut.by/rss/all.rss");
        //             foreach (var item in newsTut)
        //             {
        //                 if (newsAll.Count(c => c.LinkURL.Equals(item.LinkURL)) == 0)
        //                 {
        //                     await _mediator.Send(new AddNewsCommandModel(item));
        //                 }
        //             }
        //         },
        //         async () =>
        //         {
        //            newsS13 = await _parser.ParserNewsFrom_S13(@"http://s13.ru/rss");
        //            foreach (var s in newsS13)
        //            {
        //                if (newsAll.Count(c => c.LinkURL.Equals(s.LinkURL)) == 0)
        //                {
        //                    await _mediator.Send(new AddNewsCommandModel(s));
        //                }
        //            }
        //         }, async () =>
        //         {
        //             newsOnl = await _parser.ParserNewsFrom_Onlainer(@"https://people.onliner.by/feed");
        //             foreach (var o in newsOnl)
        //             {
        //                 if (newsAll.Count(c => c.LinkURL.Equals(o.LinkURL)) == 0)
        //                 {
        //                     await _mediator.Send(new AddNewsCommandModel(o));
        //                 }
        //             }
        //         }
        //      );
        //    return true;
        //}
    }
}
