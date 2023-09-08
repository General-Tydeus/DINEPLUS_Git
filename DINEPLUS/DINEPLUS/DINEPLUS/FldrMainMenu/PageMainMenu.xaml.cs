using Acr.UserDialogs;
using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using DINEPLUS.FldrSetup;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMainMenu : ContentPage
    {

        public static PageMainMenu Instance;
        private string strLoggedIn;
        public string strUserCode { get; set; }
        public string strUserName { get; set; }
        public string strGroupCode { get; set; }
        public string strCNCode { get; set; }

        NetworkAccess current;

        public PageMainMenu()
        {
            Instance = this;
            //current = Connectivity.NetworkAccess;
            InitializeComponent();
            OnOpeningPage();
        }

        private async void OnOpeningPage()
        {
            strLoggedIn = Preferences.Get("LogCheck", "1");

            if (strLoggedIn == "1") // 1 not ok not set
            {
                await Navigation.PushAsync(new PageLogin());
                //return;
            }
            else if (strLoggedIn == "2") //2 is OK
            {
                await LoadUserDetails();
            }
            else if (strLoggedIn == "3") //3 is OK not remember password
            {
                Preferences.Clear();
                await LoadUserDetails();
            }
            else if (string.IsNullOrEmpty(strLoggedIn))
            {
                await Navigation.PushAsync(new PageLogin());
            }

        }
        public void CheckConnection()
        {
           current = Connectivity.NetworkAccess;
        }
        public async Task LoadUserDetails()
        {
            var strSMUser = await App.ClsServeMain.GetLogInInfo();

            strCNCode = strSMUser.CNCode;
            strGroupCode = strSMUser.GroupCode;
            strUserCode = strSMUser.UserCode;
            strUserName = strSMUser.UserName;


            Preferences.Set("prefUserName", strUserName);
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            CheckConnection();
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Attention", "Make sure you have  \n Internet data access! \n before Logging Out!.", "OK");
                return;
            }
            var result = await this.DisplayAlert("Alert!", "Do you want to LogOut?", "Yes", "No");
            if (result == true)
            {
                await Logmeout();
            }
            else
            {
                await Xamarin.Forms.Application.Current.SavePropertiesAsync();
            }
        }
        private async Task Logmeout()
        {
            Preferences.Clear();
            Preferences.Set("LogCheck", "1"); // one == login page
            await Task.Delay(500);
            await Navigation.PushAsync(new PageLogin());
            var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
            var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
            foreach (var page in pageList)
                Navigation.RemovePage(page);
        }

        private async void btnPrinter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePrinterSetup());
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
                        await App.ClsServeMain.db.DeleteAllAsync<MdlTables>();
                        //await App.ClsServeMain.db.DeleteAllAsync<tblMain1Local>();
                        //await App.ClsServeMain.db.DeleteAllAsync<tblMain2Local>();
                        await Task.Delay(1000);
                        await App.ClsServeInsertLocal.SaveProduct();
                        await App.ClsServeInsertLocal.SaveDiscount();
                        await App.ClsServeInsertLocal.SaveTable();
                    }

                    //await UserDialogs.Instance.ConfirmAsync("Product Loaded Successfully", "Alert!!!", "Ok");
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
                stringBuilder.AppendLine($"Exported : {data.Exported}");
                stringBuilder.AppendLine($"Order Time  : {data.OrderTime}");

                //stringBuilder.AppendLine($"DocNum: {data.DocNum}");
                // Add other properties as needed
                stringBuilder.AppendLine(); // Add a line break between entries
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
                stringBuilder.AppendLine($"Exported  : {data.Exported}");
                stringBuilder.AppendLine($"DocNum  : {data.DocNumLocal}");
                stringBuilder.AppendLine($"Order Time  : {data.OrderTime}");
                //stringBuilder.AppendLine($"CashReceived   : {data.CashReceived}");
                //stringBuilder.AppendLine($"Serve : {data.Serve}");
                //stringBuilder.AppendLine($"TableCode : {data.TableCode}");
                //stringBuilder.AppendLine($"CAmount : {data.CAmount}");
                //stringBuilder.AppendLine($"DocNum: {data.DocNum}");
                // Add other properties as needed
                stringBuilder.AppendLine(); // Add a line break between entries
            }

            return stringBuilder.ToString();
        }

        private async void btnSetup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSetup());

        }

        private async void btnExport_Clicked(object sender, EventArgs e)
        {
            // ShowAllData();
            await Navigation.PushAsync(new PageExportList());
            
        }

        private void btnView_Clicked(object sender, EventArgs e)
        {
            ShowAllData();
        }
    }
}