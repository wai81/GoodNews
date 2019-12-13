using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Core;
using GoodNews.DB;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using MediatR;

namespace ServiceParser.Parser
{
    public class ParserSevice : IParserSevice

    {
        private const string node_TUT = "//html/body/div/div/div/div/div/div/div/div/p";
        private const string node_S13 = "//html/body/div/div/div/div/ul/li/div/div";
        private const string node_ONLAINER = "//html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/p";

        private readonly IMediator _mediator;
        
        public ParserSevice(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IEnumerable<News> ParserNewsFromSource(string source)
        {
            var news = new List<News>();
            string node_url="";
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
                    var description = ParserDescription(linkNews, node_url);
                    if (!string.IsNullOrEmpty(description))
                    {
                        Category category = new Category() { Name = postNews.Categories.FirstOrDefault().Name };
                       // string categoryName = postNews.Categories.FirstOrDefault()?.Name;
                        string title = postNews.Title.Text.Replace("&nbsp;", string.Empty);
                        string linkURL = postNews.Links.FirstOrDefault()?.Uri.ToString();
                        string content = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
                                 .Replace("Читать далее…", "");
                        //string imageURL = GetImagePost(postNews, source);
                        news.Add(new News()
                        {
                            Title = title,
                            DateCreate = postNews.PublishDate.DateTime,
                            LinkURL = linkURL,
                            NewsContent = content,
                            //CategoryName = categoryName,
                            //ImageUrl = imageURL,
                            Category =category,
                            NewsDescription = description
                        });
                    }
                }
            }
            return news;
        }


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
                    "\n\t\t\t\t\t\n\t\t\t\t\t\t\n\t\t\t\t\t\n\t\t\t\t",
                    "\r\nЧитайте также:","\r\nБиблиотека Onliner: лучшие материалы и циклы статей",
                    "\r\nНаш канал в Telegram. Присоединяйтесь!", 
                    "\r\nБыстрая связь с редакцией: читайте паблик-чат Onliner и пишите нам в Viber!",
                    "\r\nПерепечатка текста и фотографий Onliner без разрешения редакции запрещена.", "\r\n" };

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
