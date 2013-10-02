using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PickMeUp.RestAPI.Models;

namespace PickMeUp.RestAPI.Models
{
    public class ArticleModel
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Uri { get; set; }
        public virtual IEnumerable<CategoryModel> Categories { get; set; }
        public virtual IEnumerable<TagModel> Tags { get; set; }
    }
}
