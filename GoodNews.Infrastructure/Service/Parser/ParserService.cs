using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Commands.Models.Post;
using GoodNews.Infrastructure.Queries.Models.Categories;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using MediatR;

namespace GoodNews.Infrastructure.Service.Parser
{
    public class ParserService : IParserSevice

    {
        private readonly IMediator mediator;

        public ParserService(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> AddRangeAsync(IEnumerable<News> objects)
        {
            await mediator.Send(new AddRangeNewsCommandModel(objects));
            return true;
        }
      
        public async Task<IEnumerable<News>> GetNewsFromUrlAsync(string url)
        {
            List<News> news = new List<News>();
            //List<Category> category = new List<Category>();

            XmlReader xmlReader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            if (feed != null)
            {
                foreach (var postNews in feed.Items)
                {
                    string categoryName = postNews.Categories.FirstOrDefault().Name;
                    var category = await mediator.Send(new GetCategoryByNameQueryModel(categoryName));
                    if (category == null)
                    {
                       await mediator.Send(new AddCategoriCommandModel(
                            new Category()
                            {
                                Name = categoryName
                            }));
                        category = await mediator.Send(new GetCategoryByNameQueryModel(categoryName));
                    }
                    
                    var description = GetDescription(postNews.Links.FirstOrDefault().Uri.ToString());
                    if (!string.IsNullOrEmpty(description))
                    {
                        news.Add(new News()
                        {
                            Title = postNews.Title.Text.Replace("&nbsp;", string.Empty),
                            DateCreate = postNews.PublishDate.DateTime,
                            LinkURL = postNews.Links.FirstOrDefault().Uri.ToString(),
                            NewsContent = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
                                .Replace("Читать далее…", ""),
                            Category = category,
                            NewsDescription = description

                        });
                    }
                }
            }
            return news;
        }

        //public string GetDescriptionHTML(string url)
        //{
        //    string node_url = null;
        //    string content = null;

        //    if (url.IndexOf("/s13.ru/") != null)
        //    {
        //        node_url = ".js-mediator-article";
        //    }
        //    else
        //    {
        //        if (url.IndexOf(".tut.by/") != null)
        //        {
        //            node_url = "#article_body";
        //        }
        //        else
        //        {
        //            if (url.IndexOf(".onliner.by/") != null)
        //            {
        //                node_url = ".news-text";
        //            }
        //        }
        //    }
        //    var web = new HtmlWeb();
        //    var document = web.Load(url);
        //    HtmlNode nodeContent = document.QuerySelector(node_url);
        //    if (nodeContent != null)
        //    {
        //        content = nodeContent.InnerHtml;
        //    }
        //    Regex.Replace(content, @"\s+", " ");
        //    Regex.Replace(content, "Читать далее…", "");
        //    Regex.Replace(content, "<.*?>", "");
        //    Regex.Replace(content, "  ", "");
        //    Regex.Replace(content, "Библиотека Onliner: лучшие материалы и циклы статей", "");
        //    Regex.Replace(content, "Наш канал в Telegram. Присоединяйтесь!", "");
        //    Regex.Replace(content, "Быстрая связь с редакцией: читайте паблик-чат Onliner и пишите нам в Viber!", "");

        //    return HttpUtility.HtmlDecode(content);

        //}

        private string GetDescription(string url)
        {
            string node_url = null;
            string text = null;

            if (url.IndexOf("/s13.ru/", StringComparison.Ordinal) != 0)
            {
                node_url = "//html/body/div/div/div/div/ul/li/div/div";
            }
            else
            {
                if (url.IndexOf(".tut.by/", StringComparison.Ordinal) != 0)
                {
                    node_url= "//html/body/div/div/div/div/div/div/div/div/p";
                }
                else
                {
                    if (url.IndexOf(".onliner.by/", StringComparison.Ordinal) != 0)
                    {
                        node_url = "//html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/p";
                    }
                }
            }
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var node = doc.DocumentNode.SelectNodes(node_url);
            if (node != null)
            {
                foreach (var item in node)
                {
                    if (text == null)
                    {
                        text = item.InnerText;
                    }
                    else
                    {
                        text += Environment.NewLine + item.InnerText;
                    }
                }

                var mas = new string[] { "&ndash; ", "&ndash;", "&mdash; ", "&mdash;", "&nbsp; ", "&nbsp; ", "&nbsp;", "&laquo; ", "&laquo;", "&raquo; ", "&raquo;", "&quot;" };

                foreach (var item in mas)
                {
                    text = text.Replace(item, " ");
                }
                Regex.Replace(text, @"\s+", " ");
                Regex.Replace(text, "Читать далее…", "");
                Regex.Replace(text, "<.*?>", "");
                Regex.Replace(text, "  ", "");
                Regex.Replace(text, "Библиотека Onliner: лучшие материалы и циклы статей", "");
                Regex.Replace(text, "Наш канал в Telegram. Присоединяйтесь!", "");
                Regex.Replace(text, "Быстрая связь с редакцией: читайте паблик-чат Onliner и пишите нам в Viber!", "");
                return text;
            }
            return text;
        }

      
    }
}
