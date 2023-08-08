using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DINEPLUSBE.FldrClass;

namespace DINEPLUSBE.FldrLoginPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageChangePWord : ContentPage
    {
        ClsServeSecurity ClsServeSecurity1 = new ClsServeSecurity();
        //private string pristrSMCode;
        public PageChangePWord()
        {
            InitializeComponent();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            string strUserNameExist = await ClsServeSecurity1.CheckUserExists(txtUserName.Text);

            if (strUserNameExist == "1")// User name exist
            {
                string strCheckOldPWord = await ClsServeSecurity1.CheckMemberOldPWord(txtUserName.Text, txtOldPWord.Text);
                
                if (strCheckOldPWord == "1")
                {
                    PostNewPassword();
                }
                else if (strCheckOldPWord == "2")
                {
                    await DisplayAlert("Information", "Old password is invalid", "OK");
                    txtOldPWord.Focus();
                }
                else if (strCheckOldPWord == "3")
                {
                    await DisplayAlert("Information", "Please do not enter old password", "OK");
                    txtOldPWord.Focus();
                }
                else
                {
                    await DisplayAlert("Information", "Something is wrong. Possible connection error", "OK");
                }

            }
            else if (strUserNameExist == "2")//Member name not exist
            {
                await DisplayAlert("Information", "Salesman name does not exist", "OK");
                txtUserName.Focus();
            }
            else
            {
                await DisplayAlert("Information", "Something is wrong. Possible connection error1", "OK");
            }

        }

        private async void PostNewPassword()
        {
            try
            {
                if (string.IsNullOrEmpty(txtNewPWord.Text))
                {
                    await DisplayAlert("Information", "New password is empty", "OK");
                    txtNewPWord.Focus();
                }
                else if (string.IsNullOrEmpty(txtVerifyPWord.Text))
                {
                    await DisplayAlert("Information", "Verify password is empty", "OK");
                    txtVerifyPWord.Focus();
                }
                else if (txtNewPWord.Text != txtVerifyPWord.Text)
                {
                    await DisplayAlert("Information", "Verify properly", "OK");
                    txtVerifyPWord.Focus();
                }
                else
                {
                    HttpClient client = new HttpClient();
                    var result = await client.GetAsync(new ClsGetIPAddress().GetIPAddress() + "/API/SWMGLWebAPI/Login/PutNewPWord?pristrLogInName=" + txtUserName.Text + "&pristrNewPWord=" + txtNewPWord.Text);

                    if (result.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Information", "Record has been updated", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Information", "Update failed", "OK");
                        txtUserName.Focus();
                    }
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong. Possible connection error", "OK");
            }
        }
    }
}