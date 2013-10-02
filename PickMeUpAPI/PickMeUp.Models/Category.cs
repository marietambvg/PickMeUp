using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickMeUp.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }

        public virtual ICollection<Thought> Thoughts { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        
        public Category()
        {
            this.Thoughts = new HashSet<Thought>();
            this.Articles = new HashSet<Article>();
        }
    }
}
