using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Search Contract item template is documented at http://go.microsoft.com/fwlink/?LinkId=234240

namespace PickMeUpProject.Views
{
    /// <summary>
    /// This page displays search results when a global search is directed to this application.
    /// </summary>
    public sealed partial class SearchResultsPage : PickMeUpProject.Common.LayoutAwarePage
    {
        ViewModels.SearchResultsViewModel currentViewModel = null;
        public SearchResultsPage()
        {
            this.InitializeComponent();
            this.currentViewModel = new ViewModels.SearchResultsViewModel();
            this.DataContext = this.currentViewModel;
        }

        public void GridViewArticleClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ArticleContentPage), e.ClickedItem);
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
            var queryText = navigationParameter as String;
            this.currentViewModel.QueryText = queryText;

        }

    }
}
