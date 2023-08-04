
using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrEntry;
using DINEPLUSBE.FldrLoginPage;
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
       // ClsGetSomething ClsGetSomething1 = new ClsGetSomething();
        private string strLoggedIn;//1=First Time to Login In

        public PageMainMenu()
        {
            InitializeComponent();
            strLoggedIn = Preferences.Get("LogCheck", "1");
            OnOpeningPage();
        }


        //protected override bool OnBackButtonPressed()
        //{
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        var result = await this.DisplayAlert("Alert!", "Do you want to logout?", "Yes", "No");
        //        if (result == true)
        //        {
        //            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        //        }
        //        else
        //        {
        //            await Application.Current.SavePropertiesAsync();
        //        }
        //    });
        //    return true;
        //}

        //protected async override void OnAppearing()
        //{
        //    try
        //    {
        //        if (strLoggedIn == "1")
        //        {
        //            Preferences.Set("LogCheck", "2");
        //            await Navigation.PushAsync(new PageLogin());
        //        }
        //        else
        //        {
        //            string strXamOpen = await ClsGetSomething1.GetCurrentVersion();
        //            if (strXamOpen == "Yes")
        //            {
        //                //await Task.Delay(1500);
        //                //var varCurrentDealer = await App.ClsServeMain.GetCurrentDealer();
        //                //pristrCurrentDealer = varCurrentDealer.DealerCode.ToString();
        //                //LVCustList.ItemsSource = await App.ClsServeMain.GetCustomerListDealer(pristrCurrentDealer);
        //            }
        //            else if (strXamOpen == "No")
        //            {
        //                await DisplayAlert("Information", "New version is available", "OK");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        await DisplayAlert("Information", "Something went wrong. Possible error in connectionMain", "OK");
        //    }
        //}

        private async void OnOpeningPage()
        {
            try
            {
                if (strLoggedIn == "1")
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
            Preferences.Set("LogCheck", "2");
            await Task.Delay(1000);
            //await App.ClsServeMain.db.DeleteAllAsync<ClsModelDealerData>();
            //System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            await Navigation.PushAsync(new PageLogin());
        }

        private async void BtnReports_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new PageMainReports());
        }

        private async void BtnSetup_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new PageSetup());
        }

        private async void BtnPurchases_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePDName());
        }

        private void BtnAdjustment_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnEntry_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageEntryMenu());
        }

        private void BtnSecurity_Clicked(object sender, EventArgs e)
        {

        }
    }
}