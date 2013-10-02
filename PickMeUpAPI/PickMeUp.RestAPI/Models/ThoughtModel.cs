using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickMeUp.RestAPI.Models
{
    public class ThoughtModel
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Uri { get; set; }

        public virtual ICollection<CategoryModel> Categories { get; set; }


        public ThoughtModel()
        {
            this.Categories = new HashSet<CategoryModel>();
        }
    }
}