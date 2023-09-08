using Acr.UserDialogs;
using DINEPLUS.FldrClass;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using DINEPLUS.Interfaces;
using DINEPLUSWEBAPI.FldrModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePayOrder : PopupPage
    {
        public string strDocNum { get; set; }
        public string strDocNumLocal { get; set; }
        public string strGUID { get; set; }
        public static PagePayOrder Instance;
        public bool finish = false;
        public bool export { get; set; }
        NetworkAccess current;

        public readonly SQLiteAsyncConnection db;
        public PagePayOrder()
        {
            InitializeComponent();
            Instance = this;
            lblTotals.Text = $"₱{PageViewOrders.Instance.LoadSumOrd()}";
            strGUID = Guid.NewGuid().ToString();
            OnOpeningPage();
            //ShowAllData();
        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        public async void OnOpeningPage()
        {
            CheckConnection();
            if (current == NetworkAccess.Internet)
            {
                AutoNumber();
                strDocNumLocal = await App.ClsServeMain.TDocNum();
                lblDocNum.Text = strDocNumLocal;
            }
            else
            {
                strDocNumLocal = await App.ClsServeMain.TDocNum();
                lblDocNum.Text = strDocNumLocal;
            }
        }
        public async void ShowAllData()
        {
            var c = await App.ClsServeMain.ImportMain1();
            var cc = await App.ClsServeMain.ImportMain2();

            string formattedData = FormatDataListToString(c);
            string formattedData2 = FormatDataListToString2(cc);

            await DisplayAlert("All Data", $"{formattedData}{formattedData2}", "OK");
        }
        public string FormatDataListToString(List<tblMain1Local> dataList)
        {
            var stringBuilder = new StringBuilder();

            foreach (var data in dataList)
            {
                stringBuilder.AppendLine($"IC : {data.IC}");
                stringBuilder.AppendLine($"GUID: {data.GUID}");
                stringBuilder.AppendLine($"Voucher: {data.Voucher}");
                stringBuilder.AppendLine($"DocNum: {data.DocNum}");
                stringBuilder.AppendLine($"TDate : {data.TDate}");
                stringBuilder.AppendLine($"UserCode  : {data.UserCode}");
                stringBuilder.AppendLine($"Reference  : {data.Reference}");
                stringBuilder.AppendLine($"ControlNo  : {data.ControlNo}");
                stringBuilder.AppendLine($"Remarks  : {data.Remarks}");
                stringBuilder.AppendLine($"CNCode  : {data.CNCode}");
                stringBuilder.AppendLine($"CashReceived   : {data.CashReceived}");
                stringBuilder.AppendLine($"Serve : {data.Serve}");
                stringBuilder.AppendLine($"TableCode : {data.TableCode}");
                stringBuilder.AppendLine($"CAmount : {data.CAmount}");
                stringBuilder.AppendLine(); 
            }

            return stringBuilder.ToString();
        }
        public string FormatDataListToString2(List<tblMain2Local> dataList)
        {
            var stringBuilder = new StringBuilder();

            foreach (var data in dataList)
            {
                stringBuilder.AppendLine($"RowNum : {data.RowNum}");
                stringBuilder.AppendLine($"StockNumber : {data.StockNumber}");
                stringBuilder.AppendLine($"PIn : {data.PIn}");
                stringBuilder.AppendLine($"POut  : {data.POut }");
                stringBuilder.AppendLine($"UP : {data.UP}");
                stringBuilder.AppendLine($"Cost  : {data.Cost}");
                stringBuilder.AppendLine($"Discount  : {data.Discount}");
                stringBuilder.AppendLine($"Totals  : {data.Totals}");
                stringBuilder.AppendLine($"OrderTime  : {data.OrderTime}");
                stringBuilder.AppendLine($"IC  : {data.IC}");
                stringBuilder.AppendLine(); 
            }

            return stringBuilder.ToString();
        }
        private async void btnClose_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        private async void AutoNumber()
        {
            try
            {
                strDocNum = $"{await new ClsAutoNum().GetVoucherAutoNum("CS", PageMainMenu.Instance.strCNCode)}";
                //txtRef.Text = $"CS{lblDocNum.Text}";

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
                CheckConnection();
                if (current == NetworkAccess.Internet)
                {
                    export = true;
                    SaveTheTransact();
                    SaveTheLocal();
                }
                else
                {
                    export = false;
                    SaveTheLocal();
                }
        }

        public async void PrintReciept()
        {
            await PrintReceipt();
            //await DisplayAlert("1", finish.ToString(), "ok");
            while (!finish)
            {
                await Task.Delay(1000);
            }
            CleanUpAsync();
        }
        private async void SaveTheLocal()
        {
            using (UserDialogs.Instance.Loading("Printing..."))
            {
                try
                {
                 tblMain1Local localtblMain1 = tblSavetblMain1Local();
                    int n = await App.ClsServeInsertLocal.SaveMain1(localtblMain1);


                    List<tblMain2Local> listOfData = tblSavetblMain2Local();
                    foreach (var item in listOfData)
                    {
                        int result = await App.ClsServeInsertLocal.SaveMain2(item);

                        if (result != 0)
                        {
                            await DisplayAlert("alert", result.ToString(), "Ok");
                        }
                    }
                    if (n == 0)
                    {
                        PrintReciept();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to Save", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "Ok");
                }
            }
        }

        private async void SaveTheTransact()
        {
            using (HttpClient client = new HttpClient())
            {
                var tblMain1 = tblSavetblMain1(); 
                tblMain1.ModelSubtblMain2 = SavetblMain2(); 

                var content = new StringContent(JsonConvert.SerializeObject(tblMain1), Encoding.UTF8, "application/json");
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Voucher/InsertMain1", content);
                string strresult = await result.Content.ReadAsStringAsync();

                if (strresult != "1")
                {
                    await DisplayAlert("Error", strresult, "OK");
                }
            }
        }

        private async  void CleanUpAsync()
        {
            await Navigation.PopAsync();
            await PopupNavigation.Instance.PopAsync();
            clrPages();

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
                TDate = DateTime.Now.ToString("MM,dd,yyyy"),
                DocNum = strDocNum,
                Reference = $"CS{strDocNum}",
                ControlNo = "001",
                Remarks = txtRemarks.Text,
                CNCode = PageMainMenu.Instance.strCNCode,
                CashReceived = double.Parse(txtCR.Text),
                Serve = true,
                TableCode = "00",
                CAmount = double.Parse(totalOrd),
                GUID = strGUID,
                DocNumLocal = strDocNumLocal,
                Discount = double.Parse(lblDiscount.Text),
            };
        }
        public tblMain1Local tblSavetblMain1Local()
        {
            string totalOrd = lblTotals.Text;
            if (totalOrd.StartsWith("₱"))
            {
                totalOrd = totalOrd.Substring(1);
            }
            return new tblMain1Local()
            {
                //ModelSubtblMain2 = SavetblMain2(),

                Voucher = "CS",
                UserCode = PageMainMenu.Instance.strUserCode,
                TDate = DateTime.Now.ToString("MM/dd/yyyy"),
                DocNum = strDocNumLocal,
                Reference = $"CS{strDocNumLocal}",
                ControlNo = "001",
                Remarks = txtRemarks.Text,
                CNCode = PageMainMenu.Instance.strCNCode,
                CashReceived = double.Parse(txtCR.Text),
                Serve = true,
                TableCode = "00",
                CAmount = double.Parse(totalOrd),
                GUID = strGUID,
                Exported = export,
                OrderTime = DateTime.Now.ToString("hh:mm tt"),
                Discount = double.Parse(lblDiscount.Text),
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
                    OrderTime = DateTime.Now.ToString("hh:mm tt"),
                }); 
            }
            return listofData;
        }
        public List<tblMain2Local> tblSavetblMain2Local()
        {

            List<tblMain2Local> listofData = new List<tblMain2Local>();
            foreach (var vl in PagePAYO.Instance.listOrders)
            {
                listofData.Add(new tblMain2Local()
                {
                    StockNumber = vl.StockNumber,
                    PIn = 0,
                    POut = vl.Qty,
                    UP = vl.SellingPrice,
                    Cost = vl.UCost,
                    Discount = 0,
                    // RowNum = vl.RowNum,
                    OrderTime = DateTime.Now.ToString("hh:mm tt"),
                    Exported = export,
                    DocNumLocal = strDocNumLocal
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
                await DisplayAlert("Information", "Bluetooth turned off \n\nData Saved Successfully ", "OK");
                finish = true;
                return;
            }
            if (await DependencyService.Get<IBlueToothPrinterService>().CheckBlueToothPrinter() == false)
            {
                await DisplayAlert("Information", "No bluetooth device connected \nData Saved Successfully", "OK");
                finish = true;
                return;
            }

            await Task.Run(() =>
            {
                DependencyService.Get<IBlueToothPrinterService>().testPrint();
            });

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
                double disc = double.Parse(totalOrd)-(double.Parse(totalOrd) * double.Parse(PageViewOrders.Instance.strDisc));
                lblTotals.Text = $"₱{disc.ToString("n2")}";
                TotalDisc();
                return;
            }
            else
            {
                lblTotals.Text = $"₱{PageViewOrders.Instance.LoadSumOrd()}";
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
            lblDiscount.Text = (double.Parse(PageViewOrders.Instance.LoadSumOrd()) - double.Parse(totalOrd)).ToString("n2");
        }
    }
}