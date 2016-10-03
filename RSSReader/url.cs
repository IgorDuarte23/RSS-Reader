using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSSReader
{
    class url
    {

        public string Author { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int Index { get; set; }
        public string HyperLink { get; set; }

        public url(string titulo, string autor, string data, string categoria, string url, string description, int index ,string hyperLink)
        {
            Title = titulo;
            Author = autor;
            Date = data;
            Category = categoria;
            Url = url;
            Description = description;
            Index = index;
            HyperLink = hyperLink;
        }
      
    }
}
