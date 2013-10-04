using PickMeUpProject.ViewModels;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace PickMeUpProject.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ArticleContentPage : PickMeUpProject.Common.LayoutAwarePage
    {
        
        DMArticleDetailsViewModel navigationParam = new DMArticleDetailsViewModel();
        public ArticleContentPage()
        {
            this.InitializeComponent();
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    this.navigationParameter = e.Parameter as DMArticleDetailsViewModel;
        //    pageRoot.DataContext = navigationParameter;
        //}



        public async void OpenLocalFile(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            DMArticleDetailsViewModel pickedArticle = await (new LocalArticleViewModel()).GetLocalArticleData(); 
            this.Frame.Navigate(typeof(LocalArticlePage), pickedArticle);
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            this.navigationParam = navigationParameter as DMArticleDetailsViewModel;
            this.DataContext = navigationParam;
            DataTransferManager.GetForCurrentView().DataRequested += OnDataRequested;
        }
        void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            var item = this.DataContext as DMArticleDetailsViewModel;
            request.Data.Properties.Title = "The Daily Motivator - "+item.Title;
            

            // Share recipe text
            var shareMessage = item.Description;
            shareMessage += "\n\r";
            shareMessage += "Read the whole text at :";
            shareMessage += "\n\r";
            shareMessage += (item.Link);
            request.Data.SetText(shareMessage);
        }


        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            DataTransferManager.GetForCurrentView().DataRequested -= OnDataRequested;
        }
    }
}
