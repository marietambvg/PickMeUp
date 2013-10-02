using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickMeUpProject.ViewModels
{
    public class CategoryViewModel:Common.BindableBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ImageViewModel Image { get; set; }
    }
}
