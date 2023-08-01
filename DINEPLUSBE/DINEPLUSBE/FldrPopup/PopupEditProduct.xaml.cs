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
    public partial class PopupEditProduct : PopupPage
    {
        private string pristrWhatEdit;
        private string pristrStockNumber;
        //ClsValidation ClsValidation1 = new ClsValidation();
        public PopupEditProduct(string strInitProductCode, string strinitEntryField, string strinitWhatEdit)
        {
            InitializeComponent();
            pristrWhatEdit = strinitWhatEdit;
            lblEntryField.Text = strinitEntryField;
            pristrStockNumber = strInitProductCode;
            InitializeTitle();
        }

       
        private async void BtnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pristrWhatEdit == "1")
                {
                    string strResultProductDesc = await new ClsFrancisDuplicate().CheckDuplicateProduct(lblEntryField.Text);

                    if (strResultProductDesc == "1")
                    {
                        await DisplayAlert("Information", "Product exists", "OK");
                        lblEntryField.Focus();
                    }
                    else
                    {
                        ModelField ModelField1 = new ModelField()
                        {
                            StockNumber = pristrStockNumber,
                            MyFieldUpdate = lblEntryField.Text,
                            WhatToUpdate = "1",
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
                    }
                }
                else if (pristrWhatEdit == "2")
                {
                    ModelField ModelField1 = new ModelField()
                    {
                        StockNumber = pristrStockNumber,
                        MyFieldUpdate = lblEntryField.Text,
                        WhatToUpdate = "2",
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
                }
                else if (pristrWhatEdit == "4")
                {
                    ModelField ModelField1 = new ModelField()
                    {
                        StockNumber = pristrStockNumber,
                        MyFieldUpdate = lblEntryField.Text,
                        WhatToUpdate = "4",
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
                }
                else if (pristrWhatEdit == "5")
                {
                    ModelField ModelField1 = new ModelField()
                    {
                        StockNumber = pristrStockNumber,
                        MyFieldUpdate = lblEntryField.Text,
                        WhatToUpdate = "5",
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
                }
                await PopupNavigation.Instance.PopAsync();

            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong. Possible connection error", "OK");
            }
        }

        private void InitializeTitle()
        {
            if (pristrWhatEdit=="1")
            {
                lblTitle.Text = "Description";
            }
            else if (pristrWhatEdit == "2")
            {
                lblTitle.Text = "UM";
            }
            else if (pristrWhatEdit == "4")
            {
                lblTitle.Text = "Selling Price";
            }
            else if (pristrWhatEdit == "5")
            {
                lblTitle.Text = "Unit Cost";
            }
        }

        
    }
}