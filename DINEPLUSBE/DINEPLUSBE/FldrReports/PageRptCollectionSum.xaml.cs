using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrPurchases;
using DINEPLUSBE.FldrModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using DINEPLUSBE.FldrLoginPage;
using DINEPLUSBE.FldrControlPanel;
using DINEPLUSBE.fldrInterface;
using Rg.Plugins.Popup.Services;
using DINEPLUSBE.FldrPopup;

namespace DINEPLUSBE.FldrReports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRptCollectionSum : ContentPage
    {
        public static PageRptCollectionSum Instance;

        public List<ModelCollection> ItemInListView;

        public string pristrFromDate, pristrToDate;
        public PageRptCollectionSum(string strHeadFromDate, string strHeadToDate)
        {
            Instance = this;
            InitializeComponent();
            pristrFromDate = strHeadFromDate;
            pristrToDate = strHeadToDate;
        }



        protected async override void OnAppearing()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopup(), true);
                btnPrint.IsEnabled = false;
                ItemInListView = await new ClsList().GetColSum(pristrFromDate, pristrToDate);
                LV1.ItemsSource = ItemInListView;
                double dblTotalAmount = ItemInListView.Sum(x => x.CAmount);
                lblEntTotalAmt.Text = $"₱ {dblTotalAmount.ToString("N2")}";
                await PopupNavigation.Instance.PopAsync();
                btnPrint.IsEnabled = true;
            }
            catch (Exception)
            {
                await PopupNavigation.Instance.PopAsync();
                await DisplayAlert("Information", "Something went wrong, possible error in connection", "OK");
            }
        }


        private async void btnPrint_Clicked(object sender, EventArgs e)
        {
            var result = await this.DisplayAlert("Alert!", "Do you want to Print, Collection Summary?", "Yes", "No");
            if (result == true)
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
                DependencyService.Get<IBlueToothPrinterService>().PrintCSUM();

            }
            else
            {
                await Application.Current.SavePropertiesAsync();
            }
        }

    }
}

