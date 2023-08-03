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
    public partial class PopupEditCategory : PopupPage
    {
        private string pristrCatCode;
        public PopupEditCategory(string strInitCatCode, string strinitEntryField)
        {
            InitializeComponent();
            lblEntryField.Text = strinitEntryField;
            pristrCatCode = strInitCatCode;
        }

       
        private async void BtnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                    string strResultProductDesc = await new ClsFrancisDuplicate().CheckDuplicateCategory(lblEntryField.Text);

                    if (strResultProductDesc == "1")
                    {
                        await DisplayAlert("Information", "Category exists", "OK");
                        lblEntryField.Focus();
                    }
                    else
                    {
                        ModelField ModelField1 = new ModelField()
                        {
                            StockNumber = pristrCatCode,
                            MyFieldUpdate = lblEntryField.Text,
                            WhatToUpdate = "1",
                        };
                        var json = JsonConvert.SerializeObject(ModelField1);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpClient client = new HttpClient();
                        var result = await client.PutAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Entry/UpdateCategory", content);

                        if (result.IsSuccessStatusCode)
                        {
                            await Navigation.PushAsync(new PageCategoryEditList());
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