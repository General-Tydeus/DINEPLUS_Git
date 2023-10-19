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
    public partial class PageProductAdd : ContentPage
    {
        private string pristrIPAddress = new ClsGetIPAddress().GetIPAddress();
        public PageProductAdd()
        {
            InitializeComponent();
            LoadPKCatCode();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            string strResultProductDesc = await new ClsFrancisDuplicate().CheckDuplicateProduct(txtProductDesc.Text);

            if (strResultProductDesc == "1")
            {
                await DisplayAlert("Information", "Product exists", "OK");
                txtProductDesc.Focus();
            }
            else if (string.IsNullOrEmpty(txtProductDesc.Text))
            {
                await DisplayAlert("Information", "Product description is empty", "OK");
                txtProductDesc.Focus();
            }
            else if (string.IsNullOrEmpty(txtUCost.Text))
            {
                await DisplayAlert("Information", "Unit cost is empty", "OK");
                txtUCost.Focus();
            }
            else if (string.IsNullOrEmpty(txtSellingPrice.Text))
            {
                await DisplayAlert("Information", "Selling price is empty", "OK");
                txtSellingPrice.Focus();
            }
            else
            {
                var selectedItemPKCatCode = (ModeltblCategory)PKCatCode.SelectedItem;
                string strCatCode = selectedItemPKCatCode.CatCode;

                ModeltblProducts ModeltblProducts1 = new ModeltblProducts()
                {
                    ProductDesc = txtProductDesc.Text,
                    UnitMeasure = txtUnitMeasure.Text,
                    SellingPrice = double.Parse(txtSellingPrice.Text),
                    UCost = double.Parse(txtUCost.Text),
                    CatCode=strCatCode,
                    ServedDaily=cbServedDaily.IsChecked,
                };
                var json = JsonConvert.SerializeObject(ModeltblProducts1);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync($"{pristrIPAddress}/API/WebAPI/Insert/InserttblProduct", content);

                if (result.IsSuccessStatusCode)
                {
                    await DisplayAlert("Information", "Entry Saved", "OK");
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
            txtProductDesc.Text = "";
            txtUnitMeasure.Text = "";
            txtUCost.Text = "0.00";
            txtSellingPrice.Text = "0.00";
            txtProductDesc.Focus();
            lblProductCode.Text = await new ClsAutoNumber().GetProductAutoNum();
        }
        protected async override void OnAppearing()
        {
            try
            {
                lblProductCode.Text = await new ClsAutoNumber().GetProductAutoNum();
               
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }


        private async void LoadPKCatCode()
        {
            try
            {
                PKCatCode.ItemsSource = await new ClsList().GetCategory();
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible connection error3", "OK");
            }

        }

        private void txtUCost_Focused(object sender, FocusEventArgs e)
        {
            txtUCost.Text = "";
        }

        private void txtUCost_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUCost.Text))
            {
                txtUCost.Text = "0";
                txtUCost.Text = Convert.ToDouble(txtUCost.Text).ToString("N2");
            }
            else
            {
                txtUCost.Text = Convert.ToDouble(txtUCost.Text).ToString("N2");
            }
        }

        private void txtSellingPrice_Focused(object sender, FocusEventArgs e)
        {
            txtSellingPrice.Text = "";
        }

        private void txtSellingPrice_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSellingPrice.Text))
            {
                txtSellingPrice.Text = "0";
                txtSellingPrice.Text = Convert.ToDouble(txtSellingPrice.Text).ToString("N2");
            }
            else
            {
                txtSellingPrice.Text = Convert.ToDouble(txtSellingPrice.Text).ToString("N2");
            }
        }
    }
}