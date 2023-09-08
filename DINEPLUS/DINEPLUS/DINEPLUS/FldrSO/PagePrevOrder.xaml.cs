using Acr.UserDialogs;
using DINEPLUS.FldrClass;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using DINEPLUS.Interfaces;
using DINEPLUSWEBAPI.FldrModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrevOrder : ContentPage
    {
        public MdlTables mdlTables11;
        public List<tblMain2Local> PreviousOrd;
        public string strDisc;
        public static PagePrevOrder Instance;
  
        double sumOfTotals;
        NetworkAccess current;

        public PagePrevOrder(MdlTables mdlTables1)
        {
            mdlTables11 = mdlTables1;
            InitializeComponent();
            BindingContext = mdlTables1;
            Instance = this;

        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        public void clsPage3()
        {
            Navigation.RemovePage(this);
        }
        protected async override void OnAppearing()
        {
                strDisc = PageSO.Instance.strDisc;
                await LoadLV();
        }

        public async Task LoadLV()
        {
                PreviousOrd = await App.ClsServeMain.LocaltblMainTwo(mdlTables11.TableDocNum);
                LV1.ItemsSource = PreviousOrd;
                sumOfTotals = PreviousOrd.Sum(item => item.Totals);
                lblTotals.Text = $"₱{sumOfTotals:#,0.00}";
        }
        public string LoadSumOrd()
        {
            return sumOfTotals.ToString("N2");
        }
        private async void btnaddOrd_Clicked(object sender, EventArgs e)
        {
            btnaddOrd.IsEnabled = false;
            await Navigation.PushAsync(new PageProductList(mdlTables11));
            await Task.Delay(100);
            btnaddOrd.IsEnabled = true;
        }

        private async void BtnPay_Clicked(object sender, EventArgs e)
        {
            BtnPay.IsEnabled = false;
            await PopupNavigation.Instance.PushAsync(new PagePaySO(), true);
            await Task.Delay(100);
            BtnPay.IsEnabled = true;
        }

        private async void btnPrint_Clicked(object sender, EventArgs e)
        {
            btnPrint.IsEnabled = false;
            using (UserDialogs.Instance.Loading("Printing..."))
            {
                string strBTPrinterName = await App.ClsServeMain.CurrentBTPrinter();

                DependencyService.Get<IBlueToothPrinterService>().SetCurrentDevice(strBTPrinterName);
                if (DependencyService.Get<IBlueToothPrinterService>().boolBluetoothOn(strBTPrinterName) == false)
                {
                    await DisplayAlert("Information", "Bluetooth turned off", "OK");
                    return;
                }
                if (await DependencyService.Get<IBlueToothPrinterService>().CheckBlueToothPrinter() == false)
                {
                    await DisplayAlert("Information", "No bluetooth device connected", "OK");
                    return;
                }
                DependencyService.Get<IBlueToothPrinterService>().testPrintSO1();
            }
                
            btnPrint.IsEnabled = true;

        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            bool ys = await DisplayAlert("Alert!", "Are you sure you want to Delete order?", "Yes", "No");
            if (ys == true)
            {
                SwipeItem item = sender as SwipeItem;
                if (item != null)
                {
                    tblMain2Local swipedItem = item.BindingContext as tblMain2Local;

                    if (swipedItem != null)
                    {

                        App.ClsServeMain.DeleteOrder(swipedItem.DocNumLocal, swipedItem.RowNum);
                        PreviousOrd.Clear();
                        await LoadLV();
                    }
                }
            }
            
        }
    }
}