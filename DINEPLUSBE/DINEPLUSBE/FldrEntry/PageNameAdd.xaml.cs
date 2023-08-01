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
    public partial class PageNameAdd : ContentPage
    {
        public PageNameAdd()
        {
            InitializeComponent();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            string strResultDesc = await new ClsFrancisDuplicate().CheckDuplicateName(txtCustName.Text);

            if (strResultDesc == "1")
            {
                await DisplayAlert("Information", "Table duplicate", "OK");
                txtCustName.Focus();
            }
            else if (string.IsNullOrEmpty(txtCustName.Text))
            {
                await DisplayAlert("Information", "Table is empty", "OK");
                txtCustName.Focus();
            }
            else
            {
              
                ModeltblEntryName ModeltblEntryName1 = new ModeltblEntryName()
                {
                    CustName = txtCustName.Text,
                };
                var json = JsonConvert.SerializeObject(ModeltblEntryName1);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Insert/InserttblEntryName", content);

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
            txtCustName.Text = "";
            txtCustName.Focus();
            lblControlNo.Text = await new ClsAutoNumber().GetNameAutoNum();
        }
        protected async override void OnAppearing()
        {
            try
            {
                lblControlNo.Text = await new ClsAutoNumber().GetNameAutoNum();
               
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }
    }
}