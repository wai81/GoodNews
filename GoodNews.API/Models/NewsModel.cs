using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.API.Models
{
    public class NewsModel
    {
        public string Title { get; set; }// заголовок новости
        public DateTime DateCreate { get; set; }//дата новости
        public string NewsContent { get; set; }
        public string NewsDescription { get; set; }
        public string ImageUrl { get; set; }
        public string LinkURL { get; set; }
        public Guid CategoryID { get; set; }
    }
}
