using System;
using System.Collections.Generic;

namespace SongBook2.Models
{
   
    public class Song: Article
    {

        public string Content { get; set; }
        public string UrlContent { get; set; }
        public ContentType ContentType { get; set; }
        public string CoverImage { get; set; }
        public int Relevance { get; set; } //1 to 5        

        public Song()
        {
        }
    }
}
