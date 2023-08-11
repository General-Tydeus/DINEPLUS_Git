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

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePaySO : PopupPage
    {
        //string DocNum;
        public static PagePaySO Instance;
        private bool finish = false;

        public PagePaySO()
        {
            InitializeComponent();
            Instance = this;
            lblTotals.Text = $"{PagePrevOrder.Instance.lblTotals.Text}";
            txtRef.Text = $"SO";
        }

        private async void btnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
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

            // await DisplayAlert("Error", PagePrevOrder.Instance.mdlTables11.TableDocNum, "OK");

            SaveTheTransact();
            
        }
        private async void SaveTheTransact()
        {
            //await DisplayAlert("Notif", PageProductList.Instance.MdlTables11.TableCode, "OK");

            string totalOrd = lblTotals.Text;
            if (totalOrd.StartsWith("₱"))
            {
                totalOrd = totalOrd.Substring(1);
            }
            ModeltblMain1 ModeltblMain11 = new ModeltblMain1()
            {
                IC = PagePrevOrder.Instance.mdlTables11.TableDocNum,
                CashReceived = double.Parse(txtCR.Text),
                CAmount = double.Parse(totalOrd),
            };
            var json = JsonConvert.SerializeObject(ModeltblMain11);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PutAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/UpdateMain1", content);
            string strresult = await result.Content.ReadAsStringAsync();
            //await DisplayAlert("Error", strresult, "OK");
            if (strresult== "Success")
            {
                //PrintReceipt();
                //await UpdateTblStatus();
                string tblUp = await UpdateTblStatus();
                if (tblUp == "1")
                {
                    clearpages();
                }
                else
                {
                    await DisplayAlert("Error", tblUp, "OK");
                }
                // await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", strresult, "OK");
            }

        }
        public async void clearpages()
        {
            await PageSO.Instance.LoadSumary();
            PagePrevOrder.Instance.clsPage3();
            await PopupNavigation.Instance.PopAsync();
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
        public async Task<string> UpdateTblStatus()
        {

            ModeltblMain1 ModeltblMain11 = new ModeltblMain1()
            {
                TableCode = PageSO.Instance.MdlTables1.TableCode,
                TableDesc = "A",
                TableDocNum = "NA",
            };
            var json = JsonConvert.SerializeObject(ModeltblMain11);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PutAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/UpdateTblStatus", content);
            string strresult = await result.Content.ReadAsStringAsync();
            //await DisplayAlert("Notif", strresult, "OK");
            //await PopupNavigation.Instance.PopAsync();
            return strresult;
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