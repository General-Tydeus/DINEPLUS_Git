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
    public partial class PageTableAdd : ContentPage
    {
        public PageTableAdd()
        {
            InitializeComponent();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            string strResultDesc = await new ClsFrancisDuplicate().CheckDuplicateTable(txtTableDesc.Text);

            if (strResultDesc == "1")
            {
                await DisplayAlert("Information", "Table duplicate", "OK");
                txtTableDesc.Focus();
            }
            else if (string.IsNullOrEmpty(txtTableDesc.Text))
            {
                await DisplayAlert("Information", "Table is empty", "OK");
                txtTableDesc.Focus();
            }
            else
            {
              
                ModeltblTable ModeltblTable1 = new ModeltblTable()
                {
                    TableDesc = txtTableDesc.Text,
                };
                var json = JsonConvert.SerializeObject(ModeltblTable1);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WebAPI/Insert/InserttblTable", content);

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
            txtTableDesc.Text = "";
            txtTableDesc.Focus();
            lblTableCode.Text = await new ClsAutoNumber().GetTableAutoNum();
        }
        protected async override void OnAppearing()
        {
            try
            {
                lblTableCode.Text = await new ClsAutoNumber().GetTableAutoNum();
               
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

      
    }
}