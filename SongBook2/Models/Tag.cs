using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SongBook2.Models
{
    public class Tag
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }

        public Tag()
        {
        }
    }
}
