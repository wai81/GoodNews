using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodNews.DB
{
    public class News : Entity
    {  
    //public  News()
    //    {
    //        NewsComments = new HashSet<NewsComment>();
    //    }
        public string Title { get; set; }// заголовок новости
        public DateTime DateCreate { get; set; }//дата новости
        public string NewsContent { get; set; }
        public string NewsDescription { get; set; }
        public string ImageUrl { get; set; }
        public string LinkURL { get; set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<NewsComment> NewsComments { get; set; }
        public double? IndexPositive { get; set; }

    }
}
