using DINEPLUS.FldrClass;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.Interfaces;
using DINEPLUSWEBAPI.FldrModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePayOrder : PopupPage
    {
        //string DocNum;
        public static PagePayOrder Instance;
        private bool finish = false;

        public PagePayOrder()
        {
            InitializeComponent();
            Instance = this;
            lblTotals.Text = $"₱{ PageViewOrders.Instance.LoadSumOrd()}";
            AutoNumber();
        }

        private async void btnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        private async void AutoNumber()
        {
            try
            {
                lblDocNum.Text = await new ClsAutoNum().GetVoucherAutoNum("CS", PageMainMenu.Instance.strCNCode);
                txtRef.Text = $"CS{lblDocNum.Text}";

            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong. Possible error in connection", "OK");
            }

        }
        private async void BtnPost_Clicked(object sender, EventArgs e)
        {
            string totalOrd = lblTotals.Text;
            if (totalOrd.StartsWith("₱"))
            {
                totalOrd = totalOrd.Substring(1);
            }
            BtnPost.IsEnabled = false;
            btnClose.IsEnabled = false;

            if (new ClsValidation().isDouble(txtCR.Text))
            {
                BtnPost.IsEnabled = true;
                btnClose.IsEnabled = true;
                await DisplayAlert("Error", "Invalid Amount", "OK");
                txtCR.Focus();
                return;
            }
            if (double.Parse(totalOrd) > double.Parse(txtCR.Text))
            {
                BtnPost.IsEnabled = true;
                btnClose.IsEnabled = true;
                await DisplayAlert("Insufficient", "Invalid Amount", "OK");
                txtCR.Focus();
                return;
            }
            SaveTheTransact();
        }
        private async void SaveTheTransact()
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(tblSavetblMain1()), Encoding.UTF8, "application/json");
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Voucher/InsertMain1", content);
                string strresult = await result.Content.ReadAsStringAsync();

                //await DisplayAlert("Error", strresult, "OK");

                if (strresult == "1")
                {

                    var clientGet = new HttpClient();
                    clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Various/GetDoorMessage/?strWAPIVoucher=CS");
                    HttpResponseMessage response = await clientGet.GetAsync("");
                    strresult = await response.Content.ReadAsStringAsync();
                    string strresultFinal = strresult.Trim('"');
                    if (strresultFinal == "0")
                    {
                        await PrintReceipt();
                        await Navigation.PopAsync();
                        await PopupNavigation.Instance.PopAsync();
                        //while (!finish)
                        //{
                        //    await Task.Delay(100);
                        //}
                        await WaitForFinishAsync();
                        clrPages();
                    }
                    else if (strresultFinal == "1")
                    {
                        await DisplayAlert("Error", "Transaction not saved", "OK");
                    }
                    else if (strresultFinal == "2")
                    {
                        await DisplayAlert("Error", "Contact your administrator", "OK");
                    }
                    else if (strresultFinal == "3")
                    {
                        await DisplayAlert("Error", "Transaction not saved", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Failed to save", "OK");
                    return;
                }
            }

        }
        private async Task WaitForFinishAsync()
        {
            while (!finish)
            {
                await Task.Delay(100);
            }
        }
        public ModeltblMain1 tblSavetblMain1()
        {
            string totalOrd = lblTotals.Text;
            if (totalOrd.StartsWith("₱"))
            {
                totalOrd = totalOrd.Substring(1);
            }
            return new ModeltblMain1()
            {
                ModelSubtblMain2 = SavetblMain2(),

                Voucher = "CS",
                UserCode = PageMainMenu.Instance.strUserCode,
                TDate = DateTime.Now,
                DocNum = lblDocNum.Text,
                Reference = txtRef.Text,
                ControlNo = "001",
                Remarks = txtRemarks.Text,
                CNCode = PageMainMenu.Instance.strCNCode,
                CashReceived = double.Parse(totalOrd),
                Serve = true,
                TableCode = "00",
                CAmount = 0,
                //CAmount = double.Parse(lblTotals.Text),

            };
        }
        public List<ModeltblMain2> SavetblMain2()
        {

            var listofData = new List<ModeltblMain2>();
            foreach (var vl in PagePAYO.Instance.listOrders)
            {
                listofData.Add(new ModeltblMain2()
                {
                    StockNumber = vl.StockNumber,
                    PIn = 0,
                    POut = vl.Qty,
                    UP = vl.SellingPrice,
                    Cost = vl.UCost,
                    Discount = 0,
                   // RowNum = vl.RowNum,
                }); 
            }
            return listofData;
        }

        private void txtCR_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCR.Text == "")
            {
                Calcu(0);
                return;
            }
            Calcu(double.Parse(txtCR.Text));
        }
        public void Calcu(double intVal)
        {
            string totalOrd = lblTotals.Text;
            if (totalOrd.StartsWith("₱"))
            {
                totalOrd = totalOrd.Substring(1);
            }
            var sums = intVal - double.Parse(totalOrd);
            if (sums < 0)
            {
                lblChange.Text = "0.00";
                return;
            }
            lblChange.Text = sums.ToString("n2");
        }

        public void clrPages()
        {
            PagePAYO.Instance.listOrders.Clear();
            PagePAYO.Instance.LoadExp();
        }
        public async Task PrintReceipt()
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
            await Task.Run(() => DependencyService.Get<IBlueToothPrinterService>().testPrint());


            finish = true;
        }
    }
}