using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PickMeUp.RestAPI.Models;

namespace PickMeUp.RestAPI.Models
{
    public class CategoryDetailsModel : CategoryModel
    {
        public IEnumerable<ArticleModel> Articles;
        public IEnumerable<ArticleModel> Thoughts;
    }
}
