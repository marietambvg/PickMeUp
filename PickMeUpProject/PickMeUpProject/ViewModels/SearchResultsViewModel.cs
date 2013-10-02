using PickMeUpProject.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Windows.Data.Xml.Dom;

namespace PickMeUpProject.ViewModels
{
    public class SearchResultsViewModel : Common.BindableBase
    {
        private string queryText;

        public string QueryText
        {
            get
            {
                return this.queryText;
            }
            set
            {
                this.queryText = value;
                this.OnPropertyChanged("QueryText");
                this.LoadSearchedArticles("http://greatday.com/feed/dmfeed.xml");
            }
        }

        private ObservableCollection<DMArticleDetailsViewModel> searchResultArticlesCollection;

        public IEnumerable<DMArticleDetailsViewModel> SearchResultArticlesCollection
        {
            get
            {
                if (this.searchResultArticlesCollection == null)
                {
                    this.searchResultArticlesCollection = new ObservableCollection<DMArticleDetailsViewModel>();
                }

                return this.searchResultArticlesCollection;
            }
            set
            {
                if (this.searchResultArticlesCollection == null)
                {
                    this.searchResultArticlesCollection = new ObservableCollection<DMArticleDetailsViewModel>();
                }
                this.searchResultArticlesCollection.Clear();
                foreach (var item in value)
                {
                    this.searchResultArticlesCollection.Add(item);
                }

            }
        }

       
        private async void LoadSearchedArticles(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            var response = await client.GetAsync("");

            var responseText = await response.Content.ReadAsStringAsync();

            XmlDocument questions = new XmlDocument();
            questions.LoadXml(responseText);

            var items = questions.GetElementsByTagName("item");
            foreach (var item in items)
            {
                string title = "";
                string description = "";
                string link = "";


                foreach (var itemChild in item.ChildNodes)
                {
                    switch (itemChild.NodeName)
                    {
                        case "title":
                            title = itemChild.InnerText;
                            break;
                        case "link":
                            link = itemChild.InnerText;
                            break;
                        case "description":
                            description = itemChild.InnerText;
                            break;
                        default:
                            break;
                    }
                }
                var article = await DataPersister.CreateArticleDetailsViewModel(title, description, link);
                if (article.Description.ToLower().Contains(this.QueryText.ToLower()) || article.Title.ToLower().Contains(this.QueryText.ToLower())||
                    article.Content.ToLower().Contains(this.QueryText.ToLower()))
                {
                    if (this.searchResultArticlesCollection==null)
                    {
                        this.searchResultArticlesCollection = new ObservableCollection<DMArticleDetailsViewModel>();
                    }
                    this.searchResultArticlesCollection.Add(article);
                }
                
            }
        }
    }
}
