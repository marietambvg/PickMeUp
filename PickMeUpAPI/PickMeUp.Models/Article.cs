using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickMeUp.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Uri { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }


        public Article()
        {
            this.Tags = new HashSet<Tag>();
            this.Categories = new HashSet<Category>();
        }
    }
}