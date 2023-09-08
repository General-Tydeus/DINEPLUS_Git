using Acr.UserDialogs;
using DINEPLUS.FldrClass;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
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
using Xamarin.Essentials;
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
        NetworkAccess current;
        public List<tblMain2Local> localMain2;
        public string docnum { get; set; }

        public PagePaySO()
        {
            InitializeComponent();
            Instance = this;
            lblTotals.Text = $"{PagePrevOrder.Instance.lblTotals.Text}";
            lblDocNum.Text = $"{PagePrevOrder.Instance.mdlTables11.TableDocNum}";
            lblTable.Text = $"{PagePrevOrder.Instance.lblTableName.Text}";
            // txtRef.Text = $"SO";
        }
        //protected async override void OnAppearing()
        //{
        //    await DisplayAlert("table", PagePrevOrder.Instance.lblTableName.Text, "oks");
        //    //await DisplayAlert("docnum", PagePrevOrder.Instance.mdlTables11.TableDocNum, "oks");
        //}
         
        private async void btnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        private async void BtnPost_Clicked(object sender, EventArgs e)
        {
            BtnPost.IsEnabled = false;
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

            CheckConnection();
            if (current == NetworkAccess.Internet)
            {
                docnum = PagePrevOrder.Instance.mdlTables11.TableDocNum.Substring(2, PagePrevOrder.Instance.mdlTables11.TableDocNum.Length - 4);

                localMain2 = await App.ClsServeMain.localMain2Save(docnum);
                SaveTheTransact();
                SaveTheLocal();
            }
            else
            {
                SaveTheLocal();
            }
            BtnPost.IsEnabled = true;

        }
        private async void SaveTheTransact()
        {
            // string IC1 = (PagePrevOrder.Instance.mdlTables11.TableDocNum);
            try
            {
                string totalOrd = lblTotals.Text;
                if (totalOrd.StartsWith("₱"))
                {
                    totalOrd = totalOrd.Substring(1);
                }
                ModeltblMain1 ModeltblMain11 = new ModeltblMain1()
                {
                    ModelSubtblMain2 = SavetblMain2(),

                    GUID = PagePrevOrder.Instance.mdlTables11.TableGUID,
                    CashReceived = double.Parse(txtCR.Text),
                    CAmount = double.Parse(totalOrd),
                    Discount = double.Parse(lblDiscount.Text),
                    TableDocNum = PagePrevOrder.Instance.mdlTables11.TableDocNum,
                    //Serve = true,
                };
                var json = JsonConvert.SerializeObject(ModeltblMain11);
                //await DisplayAlert("Alert", json, "ok");
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/UpdateMain1", content);
                string strresult = await result.Content.ReadAsStringAsync();
                //await DisplayAlert("Error", strresult, "OK");
                //if (strresult== "Success")
                //{
                //    //await PrintReceipt();
                //    //await UpdateTblStatus();
                //    //string tblUp = await UpdateTblStatus();
                //    //if (tblUp == "1")
                //    //{
                //    //    //clearpages();
                //    //}
                //    //else
                //    //{
                //    //    await DisplayAlert("Error : Online", tblUp, "OK");
                //    //}
                //    // await Navigation.PopAsync();
                //}
                //else
                //{
                if (strresult != "Success")
                    await DisplayAlert("Error : Online", strresult, "OK");
                //}
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }

        }
        public List<ModeltblMain2> SavetblMain2()
        {

            var listofData = new List<ModeltblMain2>();
            foreach (var vl in localMain2)
            {
                if (vl.DocNumLocal == docnum)
                {
                    listofData.Add(new ModeltblMain2()
                    {
                        StockNumber = vl.StockNumber,
                        PIn = 0,
                        POut = vl.POut,
                        UP = vl.UP,
                        Cost = vl.Cost,
                        Discount = 0,
                        // RowNum = vl.RowNum,
                        OrderTime = vl.OrderTime,
                        DocNumLocal = vl.DocNumLocal,
                    });
                }
            }
            return listofData;
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
            DependencyService.Get<IBlueToothPrinterService>().testPrintSO();


            finish = true;
        }

        private void txtCR_Focused(object sender, FocusEventArgs e)
        {
            txtCR.Text = "";
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioBtn = sender as RadioButton;
            if (radioBtn.Content.ToString() == "Senior")
            {
                string totalOrd = lblTotals.Text;
                if (totalOrd.StartsWith("₱"))
                {
                    totalOrd = totalOrd.Substring(1);
                }
                double disc = double.Parse(totalOrd) - (double.Parse(totalOrd) * double.Parse(PageSO.Instance.strDisc));
                lblTotals.Text = $"₱{disc.ToString("n2")}";
                TotalDisc();
                return;
            }
            else
            {
                lblTotals.Text = $"₱{PagePrevOrder.Instance.LoadSumOrd()}";
                TotalDisc();
            }
        }
        public void TotalDisc()
        {
            string totalOrd = lblTotals.Text;
            if (totalOrd.StartsWith("₱"))
            {
                totalOrd = totalOrd.Substring(1);
            }
            lblDiscount.Text = (double.Parse(PagePrevOrder.Instance.LoadSumOrd()) - double.Parse(totalOrd)).ToString("n2");
        }
        private async void SaveTheLocal()
        {
            using (UserDialogs.Instance.Loading("Printing..."))
            { 
                string totalOrd = lblTotals.Text;
            if (totalOrd.StartsWith("₱"))
            {
                totalOrd = totalOrd.Substring(1);
            }
          
            string IC1 = PagePrevOrder.Instance.mdlTables11.TableDocNum;
            string docnum = IC1.Substring(2, IC1.Length - 4);
            double CashReceived = double.Parse(txtCR.Text);
            double CAmount = double.Parse(totalOrd);
            double disc = double.Parse(lblDiscount.Text);
            string voucher = "SO";

            string result = await App.ClsServeMain.UpdateMain1(docnum, CashReceived, CAmount, voucher, disc);
            if (result == "0")
            {
                await PrintReceipt();
                string res = await App.ClsServeMain.UpdateTable(PageSO.Instance.MdlTables1.TableCode, "A", "NA","NA");
                    await Task.Delay(1000);
                if (res== "0")
                {
                    clearpages();
                }
            }
            }
        }
    }
}