using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLogin : ContentPage
    {
        public static string glbltxtUserCode, glbltxtGroupCode, glbltxtUserName, glbltxtCNCode, glbltxtCompleteName;
        private string pristrRememberPassword;

        private void cbRemember_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            pristrRememberPassword = e.Value.ToString();
        }

        public PageLogin()
        {
            InitializeComponent();
            //string rememberedUser = Preferences.Get("rememberUser", string.Empty);
            //string rememberedPassword = Preferences.Get("rememberPass", string.Empty);
            //if (!string.IsNullOrEmpty(rememberedUser) && !string.IsNullOrEmpty(rememberedPassword))
            //{
            //    txtUserName.Text = rememberedUser;
            //    txtPassword.Text = rememberedPassword;
            //    cbRemember.IsChecked = true;
            //}
        }
        protected override void OnAppearing()
        {
            bool valueFirstMove = Preferences.Get("prefRmbPassword", false);

            if (valueFirstMove == false)
            {
            }
            else if (valueFirstMove == true)
            {
                bool valueSwitch = Preferences.Get("prefRmbPassword", false);
                cbRemember.IsChecked = valueSwitch;

                string valueLoginName = Preferences.Get("prefUserName", "A");
                txtUserName.Text = valueLoginName;

                string valuePassword = Preferences.Get("prefPassword", "B");
                txtPassword.Text = valuePassword;
            }
        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (txtUserName.Text == "")
            {
                await DisplayAlert("Username", "Please complete entry", "OK");
                return;
            }
            if (txtPassword.Text == "")
            {
                await DisplayAlert("Password", "Please complete entry", "OK");
                return;
            }
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Attention!", "Please check device connection!", "OK");
                return;
            }
            else { 
            //try
            //{
                /**string strXamOpen = await new ClsGetSomething().GetCurrentVersion();
                if (strXamOpen == "No")
                {
                    await DisplayAlert("Information", "New version is available", "OK");
                    return;
                }**/
                if (await new ClsLogData().CheckUserPWord(txtUserName.Text, txtPassword.Text) == "2")
                {
                    await DisplayAlert("Information", "Invalid Login Information", "OK");
                    return;
                }
                var varUserDetails = await new ClsGetSecurity().GetUserDetailsList(txtUserName.Text);
                glbltxtUserCode = varUserDetails.UserCode;
                glbltxtGroupCode = varUserDetails.GroupCode;
                glbltxtUserName = varUserDetails.UserName;
                glbltxtCNCode = varUserDetails.CNCode;

                //if (cbRemember.IsChecked)
                //{
                //    Preferences.Set("rememberUser", txtUserName.Text);
                //    Preferences.Set("rememberPass", txtPassword.Text);
                //}
                OpenMainMenu();



            }
            //catch (Exception)
            //{
            //    await DisplayAlert("Attention!", "Error Connection!", "OK");
            //}
        }
        private async void OpenMainMenu()
        {
            try
            {
                PreferenceTransaction();


                await App.ClsServeMain.db.DeleteAllAsync<MdlTables>();
                await Task.Delay(200);
                await App.ClsServeMain.SaveTblFunc();

                await Navigation.PushAsync(new PageMainMenu());
                var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
                foreach (var page in pageList)
                    Navigation.RemovePage(page);
            }
            catch (Exception)
            {
                await DisplayAlert("Attention!", "Posible error connnection!", "OK");
            }
        }
        private void PreferenceTransaction()
        {
            if (string.IsNullOrEmpty(pristrRememberPassword))
            {
                Preferences.Set("prefUserName", txtUserName.Text);
            }
            else
            {
                SavePreference();
            }
        }
        private void SavePreference()
        {
            if (bool.Parse(pristrRememberPassword) == true)
            {
                Preferences.Set("LogCheck", "2");// two == main

                Preferences.Set("prefRmbPassword", bool.Parse(pristrRememberPassword));

                Preferences.Set("prefUserName", txtUserName.Text);

                Preferences.Set("prefPassword", txtPassword.Text);
            }
            else if (bool.Parse(pristrRememberPassword) == false)
            {
                Preferences.Clear();
                Preferences.Set("prefUserName", txtUserName.Text);
            }
        }
    }
}