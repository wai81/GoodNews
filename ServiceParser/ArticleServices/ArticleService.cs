using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using GoodNews.DB;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace ServiceParser.ArticleServices
{
    public class ArticleService : IHtmlArticleService
    {
        private readonly IUnitOfWork _uow;
        private const string node_TUT = "//html/body/div/div/div/div/div/div/div/div/p";
        private const string node_S13 = "//html/body/div/div/div/div/ul/li/div/div";
        private const string node_ONLAINER = "//html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/p";
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }


        public bool AddRange(IEnumerable<News> articles)
        {
            _uow.NewsRepository.AddRange(articles);
            return true;

        }

        public async Task<bool> AddRangeAsync(IEnumerable<News> objects)
        {
            foreach (var item in objects)
            {
                if (!_uow.NewsRepository.Find(u => u.LinkURL.Equals(item.LinkURL)).Any())
                {
                    await _uow.NewsRepository.CreateAsync(item);
                }
            }

            return true;

        }

        public IEnumerable<News> GetNewsFromUrl(string url)
        {
            List<News> news = new List<News>();
            //List<Category> category = new List<Category>();

            XmlReader xmlReader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            if (feed != null)
            {
                foreach (var postNews in feed.Items)
                {
                    var description = GetDescription_(postNews.Links.FirstOrDefault().Uri.ToString());
                    if (!string.IsNullOrEmpty(description))
                    {
                        news.Add(new News()
                        {
                            Title = postNews.Title.Text.Replace("&nbsp;", string.Empty),
                            DateCreate = postNews.PublishDate.DateTime,
                            LinkURL = postNews.Links.FirstOrDefault().Uri.ToString(),
                            NewsContent = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
                                 .Replace("Читать далее…", ""),
                            Category = _uow.GetCategorty(postNews.Categories.FirstOrDefault().Name),
                            NewsDescription = description
                        });
                    }
                }
            }
            return news;
        }

        //public IEnumerable<News> GetArticlesFrom_Onlainer(string url)
        //{
        //    List<News> news = new List<News>();
        //    List<Category> categories = new List<Category>();
        //    XmlReader xmlReader = XmlReader.Create(url);
        //    SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
        //    if (feed != null)
        //    {
        //        foreach (var postNews in feed.Items)
        //        {
        //            var description = GetDescription(postNews.Links.FirstOrDefault().Uri.ToString(), node_ONLAINER);
        //            if (!string.IsNullOrEmpty(description))
        //            {
        //                news.Add(new News()
        //                {
        //                    Title = postNews.Title.Text,
        //                    DateCreate = postNews.PublishDate.DateTime,
        //                    LinkURL = postNews.Links.FirstOrDefault().Uri.ToString(),
        //                    NewsContent = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
        //                        .Replace("Читать далее…", ""),
        //                    Category = _uow.GetCategorty(postNews.Categories.FirstOrDefault().Name),
        //                    NewsDescription = description
        //                });
        //            }
        //        }
        //    }
        //    return news;
        //}
        //public IEnumerable<News> GetArticlesFrom_S13(string url)
        //{
        //    List<News> news = new List<News>();
        //    List<Category> categories = new List<Category>();
        //    XmlReader xmlReader = XmlReader.Create(url);
        //    SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
        //    if (feed != null)
        //    {
        //        foreach (var postNews in feed.Items)
        //        {
        //            var description = GetDescription(postNews.Links.FirstOrDefault().Uri.ToString(), node_S13);
        //            if (!string.IsNullOrEmpty(description))
        //            {
        //                news.Add(new News()
        //                {
        //                    Title = postNews.Title.Text.Replace("&nbsp;", string.Empty),
        //                    DateCreate = postNews.PublishDate.DateTime,
        //                    LinkURL = postNews.Links.FirstOrDefault().Uri.ToString(),
        //                    NewsContent = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty).Replace("Читать далее…", ""),
        //                    Category = _uow.GetCategorty(postNews.Categories.FirstOrDefault().Name),
        //                    NewsDescription = description
        //                });
        //            }
        //        }
        //    }
        //    return news;
        //}
        //public IEnumerable<News> GetArticlesFrom_TUT(string url)
        //{
        //    List<News> news = new List<News>();
        //    List<Category> categories = new List<Category>();
        //    XmlReader xmlReader = XmlReader.Create(url);
        //    SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
        //    if (feed != null)
        //    {
        //        foreach (var postNews in feed.Items)
        //        {
        //            var description = GetDescription(postNews.Links.FirstOrDefault().Uri.ToString(), node_TUT);
        //            if (!string.IsNullOrEmpty(description))
        //            {
        //                news.Add(new News()
        //                {
        //                    Title = postNews.Title.Text.Replace("&nbsp;", string.Empty),
        //                    DateCreate = postNews.PublishDate.DateTime,
        //                    LinkURL = postNews.Links.FirstOrDefault().Uri.ToString(),
        //                    NewsContent = Regex.Replace(postNews.Summary.Text, @"<[^>]+>|&nbsp;", string.Empty)
        //                        .Replace("Читать далее…", ""),
        //                    Category = _uow.GetCategorty(postNews.Categories.FirstOrDefault().Name),
        //                    NewsDescription = description
        //                });
        //            }
        //        }
        //    }
        //    return news;
        //}
        public bool Add(News article)
        {
            if (_uow.NewsRepository.Find(a => a.LinkURL.Equals(article.LinkURL)).Any())
            {
                _uow.NewsRepository.Create(article);
                return true;
            }
            _uow.NewsRepository.Create(article);
            return true;
        }

        public async Task<bool> AddAsync(News article)
        {
            if (_uow.NewsRepository.Find(a => a.LinkURL.Equals(article.LinkURL)).Any())
            {
                _uow.NewsRepository.Create(article);
                return true;
            }
            await _uow.NewsRepository.CreateAsync(article);
            return true;
        }


        //public string GetDescriptionFrom_Onlainer_QS(string url)
        //{
        //    var web = new HtmlWeb();
        //    var document = web.Load(url);

        //    HtmlNode nodeContent = document.QuerySelector(".news-text");
        //    string content="";
        //    if (nodeContent != null)
        //    {
        //        content = nodeContent.InnerHtml;
        //    }
        //    content = Regex.Replace(content, @"\s+", " ").Replace("Читать далее…", "");

        //    return HttpUtility.HtmlDecode(content);

        //}
        //public string GetDescriptionFrom_S13_QS(string url)
        //{
        //    var web = new HtmlWeb();
        //    var document = web.Load(url);

        //    HtmlNode nodeContent = document.QuerySelector(".js-mediator-article");
        //    string content = "";
        //    if (nodeContent != null)
        //    {
        //        content = nodeContent.InnerHtml;
        //    }
        //    content = Regex.Replace(content, @"\s+", " ");

        //    return HttpUtility.HtmlDecode(content);

        //}
        //public string GetDescriptionFrom_TUT_QS(string url)
        //{
        //    var web = new HtmlWeb();
        //    var document = web.Load(url);

        //    HtmlNode nodeContent = document.QuerySelector("#article_body");
        //    string content = "";
        //    if (nodeContent != null)
        //    {
        //        content = nodeContent.InnerHtml;
        //    }
        //    content = Regex.Replace(content, @"\s+", " ");

        //    return HttpUtility.HtmlDecode(content);

        //}

        //private string GetDescription(string url, string node_url)
        //{
        //    var web = new HtmlWeb();
        //    var doc = web.Load(url);

        //    string text = "";

        //    var node = doc.DocumentNode.SelectNodes(node_url);
        //    if (node != null)
        //    {

        //        foreach (var item in node)
        //        {
        //            if (text == "")
        //            {
        //                text = item.InnerText;
        //            }
        //            else
        //            {
        //                text += Environment.NewLine + item.InnerText;
        //            }
        //        }

        //        var mas = new string[] { "&ndash; ", "&ndash;", "&mdash; ", "&mdash;", "&nbsp; ", "&nbsp; ", "&nbsp;", "&laquo; ", "&laquo;", "&raquo; ", "&raquo;", "&quot;" };

        //        foreach (var item in mas)
        //        {
        //            text = text.Replace(item, " ");
        //        }



        //        Regex.Replace(text, @"\s+", " ");
        //            Regex.Replace(text, "Читать далее…", "");
        //            Regex.Replace(text, "<.*?>", "");
        //            Regex.Replace(text, "  ", "");

        //            return text;

        //    }


        //    return text;
        //}


        private string GetDescription_(string url)
        {
            string node_url = null;
            string text = null;

            if (url.Contains("s13.ru/"))
            {
                node_url = node_S13;
            }
            if (url.Contains("tut.by/"))
            {
                node_url = node_TUT;
            }
            if (url.Contains("onliner.by/"))
                node_url = node_ONLAINER;

            if (node_url == null)
            {
                return text;
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

                var mas = new string[] { "&ndash; ", "&ndash;", "&mdash; ",
                    "&mdash;", "&nbsp; ", "&nbsp; ", "&nbsp;", "&laquo; ",
                    "&laquo;", "&raquo; ", "&raquo;", "&quot;",
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
