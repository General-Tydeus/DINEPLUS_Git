using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using SQLite;
using System.IO;
using System.Collections.ObjectModel;
using System.Net;
using Rg.Plugins.Popup.Services;
using DINEPLUSBE.FldrControlPanel;
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrPopup;

namespace DINEPLUSBE.FldrEntry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageProductEdit : ContentPage
    {
        private string pristrStockNumber;
        private string pristrTogglefire = "1";//1=toggle not fire, 2=toggle fire
        private string pristrProductDesc, pristrUM, pristrCatCode, pristrSellingPrice, pristrUCost;
        public PageProductEdit(string strHeadProductCode)
        {
            InitializeComponent();
            pristrStockNumber = strHeadProductCode;
            SWTActive.Toggled += SWTActive_Toggled;
            SWTActive.IsEnabled = true;
        }


        private async void SWTActive_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (pristrTogglefire == "2")
                {
                    HttpClient client = new HttpClient();
                    var result = await client.GetAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Entry/ProductActive?strURIStockNumber={pristrStockNumber}&boolURIActive={e.Value}");
               
                    HttpResponseMessage response = await client.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                    }
                    else
                    {
                        await DisplayAlert("Information", "Failed to update record", "OK");
                    }
                    
                    
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible connection error", "OK");
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
               
                var varProducts = await new ClsList().GettblProducts("2", pristrStockNumber);
                foreach (var VL in varProducts)
                {
                    pristrProductDesc = VL.ProductDesc;
                    pristrUM = VL.UnitMeasure;
                    pristrCatCode = VL.CatCode;
                    pristrSellingPrice = VL.SellingPrice.ToString("N2");
                    pristrUCost = VL.UCost.ToString("N2");
                    lblEntProductDesc.Text = $"{VL.ProductDesc.ToString()} >>";
                    lblEntUnitM.Text = $"{VL.UnitMeasure.ToString()} >>";
                    lblEntCatDesc.Text = $"{VL.CatDesc.ToString()} >>";
                    lblEntSellingPrice.Text = $"{double.Parse(VL.SellingPrice.ToString()).ToString("N2")} >>";
                    lblEntUnitCost.Text = $"{double.Parse(VL.UCost.ToString()).ToString("N2")} >>";
                    SWTActive.IsToggled = VL.Active;
                }
                pristrTogglefire = "2";
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong, possible error in connection", "OK");
            }
        }

      
       
        private async void BtnRetMain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageMainMenu());
            var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
            foreach (var page in pageList)
                Navigation.RemovePage(page);
        }

        private async void BtnContinueEP_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageProductEditList());
            var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
            foreach (var page in pageList)
                Navigation.RemovePage(page);
        }

        private void imgbtnCatDesc_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnProductDesc_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditProduct(pristrStockNumber, pristrProductDesc, "1"), true);
        }

        private async void BtnUnitM_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditProduct(pristrStockNumber, pristrUM, "2"), true);
        }

        private async void BtnCatDesc_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditCategoryList(pristrStockNumber), true);

        }

        private async void BtnSellingPrice_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditProduct(pristrStockNumber, pristrSellingPrice, "4"), true);

        }

        private async void BtnUCost_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditProduct(pristrStockNumber, pristrUCost, "5"), true);

        }
    }
}