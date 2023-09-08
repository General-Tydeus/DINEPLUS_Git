using Acr.UserDialogs;
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
        private bool pristrRememberPassword { get; set; } = false;

    
        public PageLogin()
        { 
            InitializeComponent();
        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Logging in..."))
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
                else
                {
                    try
                    {
                        if (await new ClsLogData().CheckUserPWord(txtUserName.Text, txtPassword.Text) == "2")
                        {
                            await DisplayAlert("Information", "Invalid Login Information", "OK");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                    OpenMainMenu();
                
            }
        }
        private async void OpenMainMenu()
        {
                try
                {

                    await App.ClsServeMain.db.DeleteAllAsync<MdlTables>();
                    await Task.Delay(200);
                    await App.ClsServeMain.SaveTblFunc();

                    await App.ClsServeMain.db.DeleteAllAsync<ViewtblDetailsUser>();
                    await App.ClsServeInsertLocal.SaveLoginInfo(txtUserName.Text);


                    PreferenceTransaction();

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
            SavePreference();
        }
        private void SavePreference()
        {
            if (pristrRememberPassword)
            {
                Preferences.Set("LogCheck", "2");// two == main
                Preferences.Set("prefUserName", txtUserName.Text);
                Preferences.Set("prefPassword", txtPassword.Text);
            }
            else
            {
                Preferences.Set("LogCheck", "3");
                Preferences.Set("prefUserName", txtUserName.Text);
                Preferences.Set("prefPassword", txtPassword.Text);
            }
        }
        private void cbRemember_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            pristrRememberPassword = e.Value;
        }
        protected override void OnAppearing()
        {
            pristrRememberPassword = cbRemember.IsChecked;
            Preferences.Clear();
        }
    }
}