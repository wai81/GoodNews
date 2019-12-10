using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Core;
using GoodNews.DB;
using GoodNews.Infrastructure.Commands.Models.Categories;
using GoodNews.Infrastructure.Queries.Models;
using HtmlAgilityPack;
using MediatR;

namespace ServiceParser.Parser
{
    public class ParserSevice : IParserSevice

    {
        private const string node_TUT = "//html/body/div/div/div/div/div/div/div/div/p";
        private const string node_S13 = "//html/body/div/div/div/div/ul/li/div/div";
        private const string node_ONLAINER = "//html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/p";

        private readonly IMediator _mediator;
        private readonly IGetIndexMoodNews _getIndexMoodNews;
        public ParserSevice(IMediator mediator, IGetIndexMoodNews getIndexMoodNews)
        {
            _mediator = mediator;
            _getIndexMoodNews = getIndexMoodNews;
        }

        public IEnumerable<News> ParserNewsFromSource(string source)
        {
            var news = new List<News>();
            string node_url;
            if (source.Contains("s13.ru/rss")) node_url = node_S13;
            if (source.Contains("news.tut.by/rss")) node_url = node_TUT;
            if (source.Contains("onliner.by/feed")) node_url = node_ONLAINER;

            XmlReader xmlReader = XmlReader.Create(source);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            
            if (feed != null)
            {
                foreach (var postNews in feed.Items)
                {
                    string linkNews = postNews.Links.FirstOrDefault().Uri.ToString();
                    var description = ParserDescription(linkNews, node_S13);
                    if (!string.IsNullOrEmpty(description))
                    {
                        string categoryName = postNews.Categories.FirstOrDefault()?.Name;
                        string title = postNews.Title.Text.Replace("&nbsp;", string.Empty);
                        string linkURL = postNews.Links.FirstOrDefault()?.Uri.ToString();
                        string content = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
                                 .Replace("Читать далее…", "");
                        //double moonInd = _getIndexMoodNews.GetScore(description).Result;

                        news.Add(new News()
                        {
                            Title = title,
                            DateCreate = postNews.PublishDate.DateTime,
                            LinkURL = linkURL,
                            NewsContent = content,
                            CategoryName = categoryName,
                            NewsDescription = description
                        });
                    }
                }
            }
            return news;
        }

        public async Task<IEnumerable<News>> ParserNewsFrom_S13(string url)
        {
            List<News> news = new List<News>();
            XmlReader xmlReader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            
            if (feed != null)
            {
                foreach (var postNews in feed.Items)
                {
                    string linkNews = postNews.Links.FirstOrDefault().Uri.ToString();
                    var description = ParserDescription(linkNews, node_S13);
                    if (!string.IsNullOrEmpty(description))
                    {
                        string _category = postNews.Categories.FirstOrDefault().Name;
                        Category category =
                            await _mediator.Send(
                                new AddCategoryByNameCommandModel(_category));
                        string title = postNews.Title.Text.Replace("&nbsp;", string.Empty);
                        string linkURL = postNews.Links.FirstOrDefault().Uri.ToString();
                        string content = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
                                 .Replace("Читать далее…", "");
                        double moonInd = _getIndexMoodNews.GetScore(description).Result;

                        news.Add(new News()
                        {
                            Title = title,
                            DateCreate = postNews.PublishDate.DateTime,
                            LinkURL = linkURL,
                            NewsContent = content,
                            Category = category,
                            NewsDescription = description,
                            //MoodNews = moonInd
                        });
                    }
                }
            }
            return news;
        }
        public async Task<IEnumerable<News>> ParserNewsFrom_Onlainer(string url)
        {
            List<News> news = new List<News>();
            XmlReader xmlReader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            if (feed != null)
            {
                foreach (var postNews in feed.Items)
                {
                    string linkNews = postNews.Links.FirstOrDefault().Uri.ToString();
                    bool newsExist = await _mediator.Send(new GetNewsByUrlExistModel(linkNews));
                    if (!newsExist)
                    {
                        var description = ParserDescription(linkNews, node_ONLAINER);
                        if (!string.IsNullOrEmpty(description))
                        {

                            Category category =
                                await _mediator.Send(
                                    new AddCategoryByNameCommandModel(postNews.Categories.FirstOrDefault().Name));
                            string title = postNews.Title.Text.Replace("&nbsp;", string.Empty);
                            string linkURL = postNews.Links.FirstOrDefault().Uri.ToString();
                            string content = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
                                .Replace("Читать далее…", "");

                            double moonInd = _getIndexMoodNews.GetScore(description).Result;

                            news.Add(new News()
                            {
                                Title = title,
                                DateCreate = postNews.PublishDate.DateTime,
                                LinkURL = linkURL,
                                NewsContent = content,
                                Category = category,
                                NewsDescription = description,
                                //MoodNews = moonInd
                            }
                            );
                        }
                    }
                }
            }
            return news;
        }

        public async Task<IEnumerable<News>> ParserNewsFrom_TUT(string url)
        {
            List<News> news = new List<News>();
            XmlReader xmlReader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            var newsAll = await _mediator.Send(new GetNewsQueryModel());
            if (feed != null)
            {
                foreach (var postNews in feed.Items)
                {
                    string linkNews = postNews.Links.FirstOrDefault().Uri.ToString();
                    
                    var description = ParserDescription(linkNews, node_TUT);
                    if (!string.IsNullOrEmpty(description))
                    {

                        Category category = await _mediator.Send(new AddCategoryByNameCommandModel(postNews.Categories.FirstOrDefault().Name));
                        string title = postNews.Title.Text.Replace("&nbsp;", string.Empty);
                        string content = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
                                 .Replace("Читать далее…", "");

                        double moonInd = _getIndexMoodNews.GetScore(description).Result;

                        news.Add(new News()
                        {
                            Title = title,
                            DateCreate = postNews.PublishDate.DateTime,
                            LinkURL = linkNews,
                            NewsContent = content,
                            Category = category,
                            NewsDescription = description,
                            //MoodNews = moonInd
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

        private string ParserDescription(string url, string node_url)
        {
           
            string text = null;

           



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

                var mas = new string[] { "&ndash; ", "&ndash;", "&mdash; ",
                    "&mdash;", "&nbsp; ", "&nbsp; ", "&nbsp;", "&laquo; ",
                    "&laquo;", "&raquo; ", "&raquo;", "&quot;", "&hellip;",
                    "\n\t\t\t\t\t\n\t\t\t\t\t\t\n\t\t\t\t\t\n\t\t\t\t\n\t\t\t\t\t",
                "\r\n" , "\r\nЧитайте также:\r\nБиблиотека Onliner: лучшие материалы и циклы статей\r\nНаш канал в Telegram. Присоединяйтесь!\r\nБыстрая связь с редакцией: читайте паблик-чат Onliner и пишите нам в Viber!\r\nПерепечатка текста и фотографий Onliner без разрешения редакции запрещена." };

                foreach (var item in mas)
                {
                    text = text.Replace(item, " ");
                }
                Regex.Replace(text, @"\s+", " ");
                Regex.Replace(text, "Читать далее…", "");
                Regex.Replace(text, "<.*?>", "");
                Regex.Replace(text, "  ", "");
                Regex.Replace(text, "\r\nЧитайте также:\r\nБиблиотека Onliner: лучшие материалы и циклы статей\r\nНаш канал в Telegram. Присоединяйтесь!\r\nБыстрая связь с редакцией: читайте паблик-чат Onliner и пишите нам в Viber!\r\nПерепечатка текста и фотографий Onliner без разрешения редакции запрещена.", "");
                //Regex.Replace(text, "Наш канал в Telegram. Присоединяйтесь!", "");
                //Regex.Replace(text, "Быстрая связь с редакцией: читайте паблик-чат Onliner и пишите нам в Viber!", "");
                return text;
            }
            return text;
        }


    }
}
