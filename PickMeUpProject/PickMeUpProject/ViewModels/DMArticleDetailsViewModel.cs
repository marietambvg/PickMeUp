using PickMeUpProject.Data;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PickMeUpProject.ViewModels
{
    public class DMArticleDetailsViewModel : BaseViewModel
    {
        protected string description;


        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                this.OnPropertyChanged("Description");
            }
        }

        protected string link;
        public string Link
        {
            get
            {
                return this.link;
            }
            set
            {
                this.link = value;
                this.OnPropertyChanged("Link");
            }
        }

        protected string content;

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
                this.OnPropertyChanged("Content");
            }
        }

        protected string title;
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }

        }


        public DMArticleDetailsViewModel()
        {
            //this.LoadArticleContent("http://greatday.com");
        }



    }
}
