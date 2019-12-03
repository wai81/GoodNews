using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GoodNews.DB
{
    public class Category : Entity
    {
      public string Name { get; set; }
      public ICollection<News> News { get; set; }

    }
}
