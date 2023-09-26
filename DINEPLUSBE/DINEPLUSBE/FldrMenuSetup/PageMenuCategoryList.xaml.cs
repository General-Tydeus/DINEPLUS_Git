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
using DINEPLUSBE.FldrControlPanel;

namespace DINEPLUSBE.FldrMenuSetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageMenuCategoryList : ContentPage
	{
        public PageMenuCategoryList()
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
                    Navigation.PushAsync(new PageMenuList()
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

        private async void btnRetMain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageMainMenu());
            var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
            foreach (var page in pageList)
                Navigation.RemovePage(page);
        }

        private async void BtnInActiveAll_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool boolYes = await DisplayAlert("Information", "Are you sure?", "Yes", "Cancel");
                if (boolYes)
                {
                    var clientGet = new HttpClient();
                    clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Entry/InActiveAllProduct");

                    HttpResponseMessage response = await clientGet.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Information", "Success", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Information", "Failed to update", "OK");
                    }
                }
                else
                {

                }
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible connection error", "OK");
            }
        }
    }
}