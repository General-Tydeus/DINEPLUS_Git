using Acr.UserDialogs;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSetup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSetup : ContentPage
    {
        NetworkAccess current;

        public PageSetup()
        {
            InitializeComponent();
        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        private async void btnProduct_Clicked(object sender, EventArgs e)
        {
            CheckConnection();
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    using (UserDialogs.Instance.Loading("Loading Products..."))
                    {
                        await App.ClsServeMain.db.DeleteAllAsync<MdlProduct>();
                        await App.ClsServeMain.db.DeleteAllAsync<MdlDiscount>();
                        await App.ClsServeMain.db.DeleteAllAsync<MdlCategory>();
                        await App.ClsServeMain.db.DeleteAllAsync<MdlUser>();
                        //await App.ClsServeMain.db.DeleteAllAsync<MdlTables>();
                        //await App.ClsServeMain.db.DeleteAllAsync<tblMain1Local>();
                        //await App.ClsServeMain.db.DeleteAllAsync<tblMain2Local>();
                        await Task.Delay(1000);
                        await App.ClsServeInsertLocal.SaveProduct();
                        await App.ClsServeInsertLocal.SaveDiscount();
                        await App.ClsServeInsertLocal.SaveCategory();
                        await App.ClsServeInsertLocal.SaveUser();
                       
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                await DisplayAlert("Alert!", "Must be Connected to Internet", "Ok");
            }
        }

        private async void btnPrinter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePrinterSetup());
        }

        private async void btnTables_Clicked(object sender, EventArgs e)
        {
            CheckConnection();
            if (current == NetworkAccess.Internet)
            {
                if (await App.ClsServeMain.CheckTableAsync() == true)
                {
                    await DisplayAlert("Alert!!!", "Cannot Load Tables", "Ok");
                    return;
                }
                try
                {
                    using (UserDialogs.Instance.Loading("Loading Tables..."))
                    {
                        await App.ClsServeMain.db.DeleteAllAsync<MdlTables>();
                        await Task.Delay(1000);
                        await App.ClsServeInsertLocal.SaveTable();
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                await DisplayAlert("Alert!", "Must be Connected to Internet", "Ok");
            }
        }
    }
}