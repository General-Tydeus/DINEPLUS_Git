using DINEPLUS.FldrClass;
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

        public PageLogin()
        {
            InitializeComponent();
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
    }
}