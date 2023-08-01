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
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrModel;

namespace DINEPLUSBE.FldrEntry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCategoryAdd : ContentPage
    {
        public PageCategoryAdd()
        {
            InitializeComponent();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            string strResultDesc = await new ClsFrancisDuplicate().CheckDuplicateCategory(txtCategoryDesc.Text);

            if (strResultDesc == "1")
            {
                await DisplayAlert("Information", "Product exists", "OK");
                txtCategoryDesc.Focus();
            }
            else if (string.IsNullOrEmpty(txtCategoryDesc.Text))
            {
                await DisplayAlert("Information", "Category is empty", "OK");
                txtCategoryDesc.Focus();
            }
            else
            {
              
                ModeltblCategory ModeltblCategory1 = new ModeltblCategory()
                {
                    CatDesc = txtCategoryDesc.Text,
                };
                var json = JsonConvert.SerializeObject(ModeltblCategory1);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Insert/InserttblCategory", content);

                if (result.IsSuccessStatusCode)
                {
                    await DisplayAlert("Information", "Saved", "OK");
                    ClearScreen();
                }
                else
                {
                    await DisplayAlert("Information", "Failed to save", "OK");
                }
            }
        }

        private async void ClearScreen()
        {
            txtCategoryDesc.Text = "";
            txtCategoryDesc.Focus();
            lblCatCode.Text = await new ClsAutoNumber().GetCategoryAutoNum();
        }
        protected async override void OnAppearing()
        {
            try
            {
                lblCatCode.Text = await new ClsAutoNumber().GetCategoryAutoNum();
               
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

      
    }
}