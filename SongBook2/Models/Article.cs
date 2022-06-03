using System;
using System.Collections.Generic;

namespace SongBook2.Models
{
    public class IArticle { }

    public class Article : IArticle
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ArticleType ArticleType { get; set; }

        public DateTime CreateDate { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }

        public Article()
        {
        }
    }
}
