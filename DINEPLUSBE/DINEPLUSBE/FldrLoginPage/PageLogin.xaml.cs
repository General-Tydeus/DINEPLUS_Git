using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrControlPanel;
using DINEPLUSBE.FldrModel;

namespace DINEPLUSBE.FldrLoginPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageLogin : ContentPage
    {
        private string pristrRememberPassword;
        public PageLogin()
        {
            InitializeComponent();
            btnLogin.Clicked += BtnLogin_Clicked;
            SWRemember.Toggled += SWRemember_Toggled;
            
        }

        private void SWRemember_Toggled(object sender, ToggledEventArgs e)
        {
            pristrRememberPassword = e.Value.ToString();
        }


        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                await App.ClsServeMain.db.DeleteAllAsync<ClsModeltblUser>();

                string strResultUserName = await new ClsServeSecurity().CheckUserExists(txtLogInName.Text);
                if (strResultUserName == "1")
                {
                
                    string strResultPWord = await new ClsLogData().CheckUserPWord(txtLogInName.Text, txtPassword.Text);
                if (strResultPWord == "1")
                    {
                        await App.ClsServeMain.SaveGetCurrentUser(txtLogInName.Text);
                        PreferenceTransaction();
                        Preferences.Set("LogCheck", "2");
                        //App.IsUserLoggedIn = true;
                        await Navigation.PushAsync(new PageMainMenu());
                        
                        var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                        var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
                        foreach (var page in pageList)
                            Navigation.RemovePage(page);
                    }
                    else if (strResultPWord == "2")
                    {
                        await DisplayAlert("Information", "Password is invalid", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Information", "Something is wrong. Possible error in connection", "OK");
                    }
                }
                else if (strResultUserName == "2")
                {
                    await DisplayAlert("Information", "User name  is invalid", "OK");
                }
                else
                {
                    await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
                }
            }
            catch
            {
                await DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

        //public void AddValueSwitch(string key, string value)
        //{
        //    Preferences.Set(key, value);
        //}
        //public void AddValueLoginName(string key, string value)
        //{
        //    Preferences.Set(key, value);
        //}
        //public void AddValuePassword(string key, string value)
        //{
        //    Preferences.Set(key, value);
        //}
        //public string GetValueSwitch(string key)
        //{
        //    return Preferences.Get(key, "");
        //}
        //public string GetValueLoginName(string key)
        //{
        //    return Preferences.Get(key, "");
        //}
        //public string GetValuePassword(string key)
        //{
        //    return Preferences.Get(key, "");
        //}

        private void SavePreference()
        {
            if (bool.Parse(pristrRememberPassword) == true)
            {
                Preferences.Set("mvkev", pristrRememberPassword);
                Preferences.Set("mvkevLoginName", txtLogInName.Text);
                Preferences.Set("mvkevPassword", txtPassword.Text);
            }
        }
        protected override void OnAppearing()
        {
            string valueFirstMove = Preferences.Get("mvkev", "");
            if (string.IsNullOrEmpty(valueFirstMove))
            {

            }
            else if (bool.Parse(valueFirstMove) == true)
            {
                string valueSwitch = Preferences.Get("mkev", "true");
                SWRemember.IsToggled = bool.Parse(valueSwitch);

                string valueLoginName = Preferences.Get("mvkevLoginName", "");
                txtLogInName.Text = valueLoginName;

                string valuePassword = Preferences.Get("mvkevPassword", "");
                txtPassword.Text = valuePassword;
            }
        }

        private void PreferenceTransaction()
        {
            if (String.IsNullOrEmpty(pristrRememberPassword))
            {

            }
            else if (bool.Parse(pristrRememberPassword) == false)
            {
                Preferences.Remove("mvkev");
                Preferences.Remove("mvkevLoginName");
                Preferences.Remove("mvkevPassword");
            }
            else
            {
                SavePreference();
            }
        }

        private async void BtnChangePWord_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageChangePWord());
        }
    }
}
