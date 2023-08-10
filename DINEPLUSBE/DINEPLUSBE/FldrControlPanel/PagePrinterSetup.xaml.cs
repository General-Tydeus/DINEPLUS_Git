using DINEPLUSBE.fldrInterface;
using DINEPLUSBE.FldrModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUSBE.FldrControlPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrinterSetup : ContentPage
    {
        public PagePrinterSetup()
        {
            InitializeComponent();
        }

        private async void BtnSelectPrinter_Clicked(object sender, EventArgs e) //get all avail device
        {
            var devices = DependencyService.Get<IBlueToothPrinterService>().GetAvailableDevices();
            if (devices != null && devices.Count > 0)
            {
                var choices = devices.Select(d => d.Title).ToArray();
                string action = await Application.Current.MainPage.DisplayActionSheet("Select printer device.", "Cancel", null, choices);
                if (choices.Contains(action))
                {
                    SelectDeviceAsync(action);
                }
            }
            else
            {
                await DisplayAlert("Select Printer", "No device.", "OK");
            }
        }


        public void SelectDeviceAsync(string printerName)
        {
            App.ClsServeMain.db.DeleteAllAsync<ClsBluetoothPrinter>();
            if (DependencyService.Get<IBlueToothPrinterService>().SetCurrentDevice(printerName))
            {
                var current = DependencyService.Get<IBlueToothPrinterService>().GetCurrentDevice();
                if (current != null)
                {
                    ClsBluetoothPrinter ClsBluetoothPrinterLocal1 = new ClsBluetoothPrinter()
                    {
                        PrinterName = current.Title,
                    };
                    App.ClsServeMain.db.InsertAsync(ClsBluetoothPrinterLocal1);
                }
            }
            DisplayAlert("Information", "Successfully changed printer", "OK");
            Navigation.PopAsync();
        }

        protected async override void OnAppearing()
        {
            LblPrinterName.Text = await App.ClsServeMain.CurrentBTPrinter();
        }
    }
}