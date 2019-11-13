using System;
using System.ComponentModel.DataAnnotations;

namespace GoodNews.DB
{
    public class NewsComment : Entity
    {
        public NewsComment()
        {
            Added = DateTime.Now;
        }

        [Required]
        public User User { get; set; }
        public string Email { get; set; }

        [Required]
        public Guid NewsId { get; set; }
        public News News { get; set; }


        [Required]
        public string Text { get; set; }

        public DateTime Added { get; set; }

    }
}
