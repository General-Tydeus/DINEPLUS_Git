using Acr.UserDialogs;
using DINEPLUS.FldrModel;
using DINEPLUS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrExport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageRP : ContentPage
    {
        public static PageRP Instance;
        public List<tblMain2Local> data;
        public double disc { get; set; }
        public double cash { get; set; }
        public PageRP(string docnum, string orderTime, double discount, double cashReceived, string userCode, string date)
        {
            InitializeComponent();
            Instance = this;
            LoadLV(docnum, userCode);
            disc = discount;
            cash = cashReceived;
            lblCR.Text = cashReceived.ToString("N2");
            lblOrderTime.Text = orderTime;
            lblDisc.Text = discount.ToString("N2");
            lblChange.Text = (cashReceived - discount).ToString("N2");
            //lblCashier.Text = await App.ClsServeMain.localMain2Reprint(); 
            lblDocnum.Text = docnum;
            lblDate.Text = date;
        }
        public async void LoadLV(string docnum, string user)
        {
            lblCashier.Text = await App.ClsServeMain.GetUser(user);
            var combinedData = new List<Model123>();

            if (PageReprint.Instance.localMain2.Count <= 0)
            {
               await Navigation.PopAsync();
            }
            data = PageReprint.Instance.localMain2.Where(item => item.DocNumLocal == docnum).ToList();
            var total = data.Sum(item => item.Totals);
            LoadSumOrd(total);

            LV1.ItemsSource = null;
            LV1.ItemsSource = data;
        }
        public void LoadSumOrd(double total)
        {
            lblTotals.Text = (total - disc).ToString("N2");
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
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

                await Task.Run(() =>
                {
                    DependencyService.Get<IBlueToothPrinterService>().testRePrint();
                }); 
            }
        }
    }
}