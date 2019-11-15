using System;

namespace GoodNews.ViewModels
{
    public class CommentModel
    {
        public string commentText { get; set; }
        public Guid commentId { get; set; }
        public DateTime added { get; set; }
        public string usersName { get; set; }
        public Guid newsId { get; set; }

    }
}
