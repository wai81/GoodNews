using System.Collections.Generic;

namespace GoodNews.DB
{
    public class Category : Entity
    {
      public string Name { get; set; }
      public ICollection<News> News { get; set; }
    }
}
