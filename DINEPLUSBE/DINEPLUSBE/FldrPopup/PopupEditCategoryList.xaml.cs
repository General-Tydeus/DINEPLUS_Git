using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrEntry;
using DINEPLUSBE.FldrModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUSBE.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEditCategoryList : PopupPage
    {
        private string pristrStockNumber;
        public PopupEditCategoryList(string strInitProductCode)
        {
            InitializeComponent();
            pristrStockNumber = strInitProductCode;
            LVCategoryList.ItemSelected += LVCategoryList_ItemSelected;
        }

        private async void LVCategoryList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                string strSICatCode = (e.SelectedItem as ModeltblCategory)?.CatCode.ToString();
                ModelField ModelField1 = new ModelField()
                {
                    StockNumber = pristrStockNumber,
                    MyFieldUpdate = strSICatCode,
                    WhatToUpdate = "3",
                };
                var json = JsonConvert.SerializeObject(ModelField1);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PutAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Entry/UpdateProduct", content);

                if (result.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new PageProductEdit(pristrStockNumber));
                    var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
                    foreach (var page in pageList)
                        Navigation.RemovePage(page);
                }

                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong. Possible connection error", "OK");
            }
        }

    
        private async void BtnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        protected async override void OnAppearing()
        {
            try
            {
                LVCategoryList.ItemsSource = await new ClsList().GetCategory();
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong, possible error in connection", "OK");
            }
        }
    }
}