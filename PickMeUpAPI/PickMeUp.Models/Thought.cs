using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickMeUp.Models
{
    public class Thought
    {

        public int ID { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Uri { get; set; }

        public virtual ICollection<Category> Categories { get; set; }


        public Thought()
        {
            this.Categories = new HashSet<Category>();
        }
    }
}