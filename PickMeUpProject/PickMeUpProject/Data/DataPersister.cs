using PickMeUpProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PickMeUpProject.Data
{
    public class DataPersister
    {
        protected static string AccessToken { get; set; }

        private const string BaseServicesUrl = "http://localhost:1405/api/";

        public static Task<IEnumerable<CategoryViewModel>> GetCategories()
        {

            var categoryListsModels =
                HttpRequest.Get<IEnumerable<CategoryViewModel>>(BaseServicesUrl + "category");

            return categoryListsModels;
        }

        public static async Task<string> LoadArticleContent(string url)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            var response = await client.GetAsync("");

            var result = await response.Content.ReadAsStringAsync();

            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(result);
            string contentPicker = "";
            var mainContent = document.GetElementbyId("maincontent");
            //if (this.title == null)
            //{
            //    this.title = mainContent.DescendantNodes().Where(x => x.Name == "h1").Select(a => a.InnerText).FirstOrDefault();
            //}

            var paragraphs = mainContent.DescendantNodes().Where(x => x.Name == "p").Select(a => a.InnerText).ToList();

            for (int i = 1; i < paragraphs.Count - 4; i++)
            {
                if (!paragraphs[i].Contains("&"))
                {
                    contentPicker += paragraphs[i] + Environment.NewLine;
                }
            }
            return contentPicker;

        }

        public static async Task<DMArticleDetailsViewModel> CreateArticleDetailsViewModel(string title, string description, string link)
        {
            DMArticleDetailsViewModel article = new DMArticleDetailsViewModel();
            article.Title = title;
            article.Description = description;
            article.Link = link;
            article.Content = await LoadArticleContent(link);
            return article;
        }

        public static async Task<ObservableCollection<DMArticleDetailsViewModel>> LoadRecentArticles(string url, ObservableCollection<DMArticleDetailsViewModel> articles)
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
                articles.Add(article);
            }
            return articles;
        }

        public static XDocument ToXML(DMArticleDetailsViewModel article)
        {
            XDocument document = new XDocument(
                new XElement("root",
                    new XElement("title", article.Title),
                    new XElement("description", article.Description),
                    new XElement("link", article.Link),
                     new XElement("content", article.Content)

                )
            );
            return document;
        }

        public static async Task WriteXml<T>(T article, StorageFile file)
        {
            try
            {
                StringWriter sw = new StringWriter();
                XmlSerializer xmlser = new XmlSerializer(typeof(T));
                xmlser.Serialize(sw, article);

                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (IOutputStream outputStream = fileStream.GetOutputStreamAt(0))
                    {
                        using (DataWriter dataWriter = new DataWriter(outputStream))
                        {
                            dataWriter.WriteString(sw.ToString());
                            await dataWriter.StoreAsync();
                            dataWriter.DetachStream();
                        }

                        await outputStream.FlushAsync();
                    }
                }

            }
            catch (Exception e)
            {
                throw new NotImplementedException(e.Message.ToString());

            }

        }

        public static async Task SaveXml<T>(T article)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();

            var xmlFileTypes = new List<string>(new string[] { ".xml" });



            savePicker.FileTypeChoices.Add(
                new KeyValuePair<string, IList<string>>("XML File", xmlFileTypes)
                );


            var saveFile = await savePicker.PickSaveFileAsync();


            if (saveFile != null)
            {

                await WriteXml(article, saveFile);
            }
        }

        public static async Task SaveFile(XDocument text)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();

            var xmlFileTypes = new List<string>(new string[] { ".xml" });



            savePicker.FileTypeChoices.Add(
                new KeyValuePair<string, IList<string>>("XML File", xmlFileTypes)
                );


            var saveFile = await savePicker.PickSaveFileAsync();


            if (saveFile != null)
            {
                await Windows.Storage.FileIO.WriteTextAsync(saveFile, text.ToString());


                await new Windows.UI.Popups.MessageDialog("File Saved!").ShowAsync();
            }
        }
    }
}