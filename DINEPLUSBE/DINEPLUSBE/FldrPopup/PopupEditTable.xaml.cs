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
    public partial class PopupEditTable : PopupPage
    {
        private string pristrTableCode;
        public PopupEditTable(string strInitTableCode, string strinitEntryField)
        {
            InitializeComponent();
            lblEntryField.Text = strinitEntryField;
            pristrTableCode = strInitTableCode;
        }

       
        private async void BtnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                    string strResultProductDesc = await new ClsFrancisDuplicate().CheckDuplicateTable(lblEntryField.Text);

                    if (strResultProductDesc == "1")
                    {
                        await DisplayAlert("Information", "Duplicate table", "OK");
                        lblEntryField.Focus();
                    }
                    else
                    {
                        ModelField ModelField1 = new ModelField()
                        {
                            StockNumber = pristrTableCode,
                            MyFieldUpdate = lblEntryField.Text,
                            WhatToUpdate = "1",
                        };
                        var json = JsonConvert.SerializeObject(ModelField1);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpClient client = new HttpClient();
                        var result = await client.PutAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Entry/UpdateTable", content);

                        if (result.IsSuccessStatusCode)
                        {
                            await Navigation.PushAsync(new PageTableEditList());
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
    }
}