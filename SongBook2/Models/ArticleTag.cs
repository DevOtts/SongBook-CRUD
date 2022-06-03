using System;
using System.Collections.Generic;

namespace SongBook2.Models
{

    public class ArticleTag
    {

        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }

        public Tag Tag { get; set; }
        public string TagId { get; set; }

        public ArticleTag()
        {
        }
    }
}
