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
	public partial class PageProductEditList : ContentPage
	{
        public PageProductEditList()
		{
			InitializeComponent ();
            LVProductList.ItemSelected += LVProductList_ItemSelected;
		}

        private async void LVProductList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                string strStockNumber = (e.SelectedItem as ModeltblProducts)?.StockNumber.ToString();
                await Navigation.PushAsync(new PageProductEdit(strStockNumber));
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
            LVProductList.ItemsSource = await new ClsList().GettblProducts("1", "Nothing");
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong. Possible error in connection", "OK");
            }

        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ModeltblProducts> ModeltblProducts1;
            ModeltblProducts1 = await new ClsList().GettblProducts("1", "Nothing");
            LVProductList.ItemsSource = ModeltblProducts1.Where(x => x.ProductDesc.ToLower().Contains(e.NewTextValue)).ToList();
        }

        private async void BtnInActiveAll_Clicked(object sender, EventArgs e)
        {
            bool boolYes = await DisplayAlert("Information", "Are you sure?", "Yes", "Cancel");
            if (boolYes)
            {
                var clientGet = new HttpClient();
                clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}API/WebAPI/Entry/InActiveAllProduct");

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
    }
}