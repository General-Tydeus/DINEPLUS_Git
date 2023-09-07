
using DINEPLUSBE.FldrAdjustment;
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrEntry;
using DINEPLUSBE.FldrLoginPage;
using DINEPLUSBE.FldrPurchases;
using DINEPLUSBE.FldrReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DINEPLUSBE.FldrControlPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMainMenu : ContentPage
    {
        private string strLoggedIn;//1=First Time to Login In

        public PageMainMenu()
        {
            InitializeComponent();
            
            OnOpeningPage();
        }


        private async void OnOpeningPage()
        {
            try
            {
                var varUserName = await App.ClsServeMain.GetCurrentUser();
                string strUCode = varUserName.UserCode.ToString();
                strLoggedIn = Preferences.Get("LogCheck", "");
                if (string.IsNullOrEmpty(strLoggedIn))
                {
                    Preferences.Set("LogCheck", "2");
                    await Navigation.PushAsync(new PageLogin());
                }
                else if (strLoggedIn == "1")
                {
                    Preferences.Set("LogCheck", "2");
                    await Navigation.PushAsync(new PageLogin());
                }
                else if (string.IsNullOrEmpty(strUCode))
                {
                    Preferences.Set("LogCheck", "2");
                    await Navigation.PushAsync(new PageLogin());
                }
                
            }
            catch (Exception)
            {
                //await DisplayAlert("Information", "Something went wrong. Possible error in connectionMain", "OK");
            }
        }

        
        private async void BtnLogout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("LogCheck", "1");
            await Task.Delay(1000);
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            //Preferences.Set("mvkev", "true");

            await Navigation.PushAsync(new PageLogin());
        }

        private async void BtnReports_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageReportMenu());
        }

        private async void BtnPurchases_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePDName());
        }

        private async void BtnAdjustment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageASName());
        }

        private async void BtnEntry_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageEntryMenu());
        }

        private void BtnSecurity_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnPrinter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePrinterSetup());
        }
    }
}