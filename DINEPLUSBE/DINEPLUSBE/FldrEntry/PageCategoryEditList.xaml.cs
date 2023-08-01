using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net;
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrModel;

namespace DINEPLUSBE.FldrEntry
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageCategoryEditList : ContentPage
	{
        public PageCategoryEditList()
		{
			InitializeComponent ();
            LVCategoryList.ItemSelected += LVCategoryList_ItemSelected;
		}

        private  void LVCategoryList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    Navigation.PushAsync(new PageCategoryEdit()
                    {
                        BindingContext = e.SelectedItem as ModeltblCategory
                    });
                }
            }
            catch (Exception)
            {
                DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
                LVCategoryList.ItemsSource = await new ClsList().GetCategory();
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ModeltblCategory> ModeltblCategory1;
            ModeltblCategory1 = await new ClsList().GetCategory();
            LVCategoryList.ItemsSource = ModeltblCategory1.Where(x => x.CatDesc.ToLower().Contains(e.NewTextValue)).ToList();
        }

    }
}