using PickMeUpProject.Behavior;
using PickMeUpProject.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Controls;

namespace PickMeUpProject.ViewModels
{
    public class DMRecentArticlesViewModel : Common.BindableBase, IPageViewer
    {

        private ICommand saveArticleToFileCommand;
        public ICommand SaveArticleToFile
        {
            get
            {
                if (this.saveArticleToFileCommand == null)
                {
                    this.saveArticleToFileCommand = new DelegateCommand<object>(this.HandleSaveArticleToFileCommand);
                }
                return this.saveArticleToFileCommand;
            }
        }

        private async void HandleSaveArticleToFileCommand(object parameter)
        {
            var stackPanel = (parameter as Button).Parent;
            DMArticleDetailsViewModel currentArticle = (stackPanel as StackPanel).DataContext as DMArticleDetailsViewModel;
            XDocument text = DataPersister.ToXML(currentArticle);
            await DataPersister.SaveFile(text);
        }

        
        public string Name
        {
            get
            {
                return "Resent Articles";
            }
        }

        private ObservableCollection<DMArticleDetailsViewModel> recentArticlesCollection;

        public IEnumerable<DMArticleDetailsViewModel> RecentArticlesCollection
        {
            get
            {
                if (this.recentArticlesCollection == null)
                {
                    this.recentArticlesCollection = new ObservableCollection<DMArticleDetailsViewModel>();
                }

                return this.recentArticlesCollection;
            }
            set
            {
                if (this.recentArticlesCollection == null)
                {
                    this.recentArticlesCollection = new ObservableCollection<DMArticleDetailsViewModel>();
                }
                this.recentArticlesCollection.Clear();
                foreach (var item in value)
                {
                    this.recentArticlesCollection.Add(item);
                }

            }
        }

        public string Content { get; set; }

        public DMRecentArticlesViewModel()
        {
            this.Content = "Here's To The Crazy Ones.\nThe misfits. \nThe rebels.\nThe trouble-makers." +
                            "\nThe round pegs in the quare holes. \nThe ones who see things differently. \nThey're not fond of rules, and they have " +
                            "no respect for the status-quo. \nYou can quote them, disagree with them, glorify, or vilify them." +
                            "\nAbout the only thing you can't do is ignore them. \nBecause they change things. \nThey push the " +
                            "human race forward. \nAnd while some may see them as the crazy ones, we see genius." +
                            "\nBecause the people who are crazy enough to think they can change the world - \nare the ones who DO !";
            this.RecentArticlesCollection = new ObservableCollection<DMArticleDetailsViewModel>();

            this.LoadRecentArticles("http://greatday.com/feed/dmfeed.xml");

        }

        private async void LoadRecentArticles(string url)
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
                this.recentArticlesCollection.Add(article);
            }
        }
    }
}

