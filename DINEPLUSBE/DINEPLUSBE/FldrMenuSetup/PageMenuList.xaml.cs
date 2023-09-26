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

namespace DINEPLUSBE.FldrMenuSetup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMenuList : ContentPage
    {
        public PageMenuList()
        {
            InitializeComponent();
            LVProductList.ItemSelected += LVProductList_ItemSelected;
        }

        private async void LVProductList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                string strStockNumber = (e.SelectedItem as ModeltblProducts)?.StockNumber.ToString();
                await Navigation.PushAsync(new PageMenuFortheDay(strStockNumber));
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
                LVProductList.ItemsSource = await new ClsList().GetCategoryProductsDynamic(lblCatCode.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ModeltblProducts> ModeltblProducts1;
            ModeltblProducts1 = await new ClsList().GetCategoryProductsDynamic(lblCatCode.Text);
            LVProductList.ItemsSource = ModeltblProducts1.Where(x => x.ProductDesc.ToLower().Contains(e.NewTextValue)).ToList();
        }

    }
}