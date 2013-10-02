using PickMeUpProject.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace PickMeUpProject.ViewModels
{
    public class CategoriesViewModel:BaseViewModel,IPageViewer
    {
        
        private ObservableCollection<CategoryViewModel> categories;
        private string title;

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

        public string Name
        {
            get
            {
                return "Categories";
            }
        }

        public IEnumerable<CategoryViewModel> Categories
        {
            get
            {
                if (this.categories == null)
                {
                    this.categories = new ObservableCollection<CategoryViewModel>();
                    this.GetData();
        
                }
                return this.categories;
            }
            set
            {
                if (this.categories == null)
                {
                    this.categories = new ObservableCollection<CategoryViewModel>();
                }
                this.SetObservableValues(this.categories, value);
            }
        }

        protected async void GetData()
        {
            this.Categories = await DataPersister.GetCategories();
               
        }

    }
}

    