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

namespace DINEPLUSBE.FldrSecurity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageUserAdd : ContentPage
    {
        private string pristrIPAddress = new ClsGetIPAddress().GetIPAddress();
        public PageUserAdd()
        {
            InitializeComponent();
            LoadPKGroupCode();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            string strResultUserName = await new ClsFrancisDuplicate().CheckDuplicateUserName(txtUserName.Text);

            if (strResultUserName == "1")
            {
                await DisplayAlert("Information", "User name exists", "OK");
                txtUserName.Focus();
            }
            else if (string.IsNullOrEmpty(txtCompleteName.Text))
            {
                await DisplayAlert("Information", "Complete name is empty", "OK");
                txtCompleteName.Focus();
            }
            else if (PKGroupCode.SelectedIndex == -1)
            {
                await DisplayAlert("Information", "Group is empty", "OK");
                PKGroupCode.Focus();
            }
            else
            {
                OnAppearing();
                var selectedItemPKGroupCode = (ModeltblGroup)PKGroupCode.SelectedItem;
                string strGroupCode = selectedItemPKGroupCode.GroupCode;

                ModeltblUser ModeltblUser1 = new ModeltblUser()
                {
                    GroupCode = strGroupCode,
                    UserName = txtUserName.Text,
                    CompleteName=txtCompleteName.Text,
                    CNCode = "01",
                };
                var json = JsonConvert.SerializeObject(ModeltblUser1);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/WEBAPI/WEBAPISecurity/InsertUser", content);
                if (result.IsSuccessStatusCode)
                {

                    await DisplayAlert("Information", "Successfully saved the user", "OK");
                    OnAppearing();
                    PKGroupCode.SelectedIndex = -1;
                    txtUserName.Text="";
                    txtCompleteName.Text = "";
                    txtUserName.Focus();
                }
                else
                {
                    await DisplayAlert("Information", "Failed", "OK");
                }
            }
        }
    protected async override void OnAppearing()
        {
            try
            {
                lblUserCode.Text = await new ClsAutoNumber().GetUserCodeNumber();
               
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }


        private async void LoadPKGroupCode()
        {
            try
            {
                PKGroupCode.ItemsSource = await new ClsList().GetGroupList();
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something went wrong. Possible connection error", "OK");
            }

        }

    }
}