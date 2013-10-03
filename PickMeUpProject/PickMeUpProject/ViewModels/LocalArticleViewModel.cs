using System.IO;
using System.Xml.Serialization;
using PickMeUpProject.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace PickMeUpProject.ViewModels
{
    public class LocalArticleViewModel : Common.BindableBase
    {


        public DMArticleDetailsViewModel LocalArticle { get; set; }
        

        public LocalArticleViewModel()
        {
            this.GetLocalArticleData();
        }

        public async void GetLocalArticleData()
        {
            StorageFile file = await OpenFile();
            this.LocalArticle = await LoadLocalArticles(file);
        }

        private async Task<StorageFile> OpenFile()
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            openPicker.FileTypeFilter.Add(".xml");

            StorageFile file = await openPicker.PickSingleFileAsync();
            return file;
        }

        private async Task<T> ReadXml<T>(StorageFile xmldata)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<T>));
            T data;
            using (var strm = await xmldata.OpenStreamForReadAsync())
            {
                TextReader Reader = new StreamReader(strm);
                data = (T)xmlser.Deserialize(Reader);
            }
            return data;
        }

        private async Task<DMArticleDetailsViewModel> LoadLocalArticles(StorageFile file)
        {
            var text = await Windows.Storage.FileIO.ReadTextAsync(file);


            XmlDocument questions = new XmlDocument();
            questions.LoadXml(text);
            DMArticleDetailsViewModel pickedArticle = new DMArticleDetailsViewModel();
            var items = questions.GetElementsByTagName("DMArticleDetailsViewModel");
            foreach (var item in items)
            {

                foreach (var itemChild in item.ChildNodes)
                {
                    switch (itemChild.NodeName)
                    {
                        case "Title":
                            pickedArticle.Title = itemChild.InnerText;
                            break;
                        case "Link":
                            pickedArticle.Link = itemChild.InnerText;
                            break;
                        case "Description":
                            pickedArticle.Description = itemChild.InnerText;
                            break;
                        case "Content":
                            pickedArticle.Content = itemChild.InnerText;
                            break;
                        default:
                            break;
                    }
                }

            }
            return pickedArticle;
        }
    }
}
