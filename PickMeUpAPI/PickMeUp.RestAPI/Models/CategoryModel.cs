using System.Collections.Generic;
using PickMeUp.RestAPI.Models;

namespace PickMeUp.RestAPI.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ImageModel Image { get; set; }
    }
}
