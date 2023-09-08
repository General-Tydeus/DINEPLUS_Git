using DINEPLUS.FldrClass;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using DINEPLUSWEBAPI.FldrModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpTakeOrd : PopupPage
    {
        //public string strDocNum { get; set; }
        public string MyReference { get; set; }
        public string strDocNumLocal { get; set; }
        public string prevDocNumLocal { get; set; }
        public string strGUID { get; set; }
        public bool export { get; set; }
        NetworkAccess current;
         
        public PopUpTakeOrd()
        {
            InitializeComponent();
            lblTotals.Text = $"₱ {PageSOCart.Instance.LoadSumOrd()}";
            strGUID = Guid.NewGuid().ToString();
        }
        protected async override void OnAppearing()
        {
          strDocNumLocal = await App.ClsServeMain.TDocNum();
        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        private void btnClose_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private async void BtnPost_Clicked(object sender, System.EventArgs e)
        {
            BtnPost.IsEnabled = false;
            if (String.IsNullOrWhiteSpace(txtRemarks.Text))
            {
                await DisplayAlert("info","please complete entry!","OK");
                return;
            }

            if (PageSO.Instance.Additional == "New")
            {
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
            else if (PageSO.Instance.Additional == "Additional")
            {
                CheckConnection();
                if (current == NetworkAccess.Internet)
                {
                    string IC1 = PagePrevOrder.Instance.mdlTables11.TableDocNum;
                    string docnum = IC1.Substring(2, IC1.Length - 4);
                    prevDocNumLocal = docnum;
                    export = true;
                    SaveAdditional();
                    SaveNewOrder();
                }
                else
                {
                    string IC1 = PagePrevOrder.Instance.mdlTables11.TableDocNum;
                    string docnum = IC1.Substring(2, IC1.Length - 4);
                    prevDocNumLocal = docnum;
                    export = false;
                    SaveNewOrder();
                }
               
            }
            BtnPost.IsEnabled = true;
        }

        public ModeltblMain1 tblSavetblMain1()
        {
            return new ModeltblMain1()
            {
                ModelSubtblMain2 = SavetblMain2(),

                //IC = $"SO{MyDocNum}",
                Voucher = "SO",
                DocNum = strDocNumLocal,
                UserCode = PageMainMenu.Instance.strUserCode,
                TDate = DateTime.Now.ToString("MM,dd,yyyy"),
                Reference = $"SO{strDocNumLocal}",
                ControlNo = "001",
                Remarks = txtRemarks.Text,
                CNCode = PageMainMenu.Instance.strCNCode,
                TableCode = PageProductList.Instance.MdlTables11.TableCode,
                CashReceived = 0,
                Serve = false,
                //TableDesc = PageProductList.Instance.MdlTables11.TableDesc,
                CAmount = 0,
                GUID = strGUID,
                DocNumLocal = strDocNumLocal,
                //CAmount = double.Parse(lblTotals.Text),

            };
        }
        public List<ModeltblMain2> SavetblMain2()
        {
            var listofData = new List<ModeltblMain2>();
            foreach (var vl in PageProductList.Instance.listOrders)
            {
                listofData.Add(new ModeltblMain2()
                {
                    //MyDocNumIC = PagePrevOrder.Instance.mdlTables11.TableDocNum,
                    //DocNum = MyDocNum,
                    StockNumber = vl.StockNumber,
                    PIn = 0,
                    POut = vl.Qty,
                    UP = vl.SellingPrice,
                    Cost = vl.UCost,
                    Discount = 0,
                    //ProductDesc = vl.ProductDesc,
                    Totals = vl.Totals,
                    OrderTime = DateTime.Now.ToString("hh:mm tt")
                });
            }
            return listofData;
        }
        
        public List<ModeltblMain2> SavetblMain21()
        {
            var listofData = new List<ModeltblMain2>();
            foreach (var vl in PageProductList.Instance.listOrders)
            {
                listofData.Add(new ModeltblMain2()
                {
                    IC = PagePrevOrder.Instance.mdlTables11.TableDocNum,
                    //DocNum = MyDocNum,
                    StockNumber = vl.StockNumber,
                    PIn = 0,
                    POut = vl.Qty,
                    UP = vl.SellingPrice,
                    Cost = vl.UCost,
                    Discount = 0,
                    //ProductDesc = vl.ProductDesc,
                    Totals = vl.Totals,
                    OrderTime = DateTime.Now.ToString("hh:mm tt")
                });
            }
            return listofData;
        }


        private async void SaveTheTransact()
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(tblSavetblMain1()), Encoding.UTF8, "application/json");
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Voucher/InsertMain1", content);

                string strresult = await result.Content.ReadAsStringAsync();

                if (strresult == "1")
                {
                }
                else
                {
                    await DisplayAlert("Error", "Failed to save", "OK");
                    return;
                }
            }
        }
        private async void SaveTheLocal()
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
                    UpdateLocalTable();
                    clrpgs();
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
        public tblMain1Local tblSavetblMain1Local()
        {
          
            return new tblMain1Local()
            {
                Voucher = "SO",
                UserCode = PageMainMenu.Instance.strUserCode,
                TDate = DateTime.Now.ToString("MM/dd/yyyy"),
                DocNum = strDocNumLocal,
                Reference = $"SO{strDocNumLocal}",
                ControlNo = "001",
                Remarks = txtRemarks.Text,
                CNCode = PageMainMenu.Instance.strCNCode,
                CashReceived = 0,
                Serve = false,
                TableCode = PageProductList.Instance.MdlTables11.TableCode,
                CAmount = 0,
                GUID = strGUID,
                Exported = export,
                OrderTime = DateTime.Now.ToString("hh:mm tt"),
            };
        }
        public List<tblMain2Local> tblSavetblMain2Local()
        {

            List<tblMain2Local> listofData = new List<tblMain2Local>();
            foreach (var vl in PageProductList.Instance.listOrders)
            {
                listofData.Add(new tblMain2Local()
                {
                    IC = $"SO{strDocNumLocal}{PageMainMenu.Instance.strCNCode}",
                    StockNumber = vl.StockNumber,
                    PIn = 0,
                    POut = vl.Qty,
                    UP = vl.SellingPrice,
                    Cost = vl.UCost,
                    Discount = 0,
                    Totals = vl.Totals,
                    ProductDesc = vl.ProductDesc,
                    // RowNum = vl.RowNum,
                    OrderTime = DateTime.Now.ToString("hh:mm tt"),
                    Exported = export,
                    DocNumLocal = strDocNumLocal,
                });
            }
            return listofData;
        }
        public async Task UpdateTblStatus()
        {
            ModeltblMain1 ModeltblMain11 = new ModeltblMain1()
            {
                TableCode = PageProductList.Instance.MdlTables11.TableCode,
                TableDesc = "O",
                TableDocNum = $"SO{strDocNumLocal}{PageMainMenu.Instance.strCNCode}",
            };
            var json = JsonConvert.SerializeObject(ModeltblMain11);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PutAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/UpdateTblStatus", content);
            string strresult = await result.Content.ReadAsStringAsync();
        }

        public void UpdateLocalTable()
        {
            string docnum = $"SO{strDocNumLocal}{PageMainMenu.Instance.strCNCode}";
            App.ClsServeMain.TableOccupied(tblSavetblMain1Local().TableCode, docnum, tblSavetblMain1Local().GUID);

        }
        public async void SaveAdditional()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(SavetblMain21()), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Voucher/InsertMain2Additional", content);
                    string strresult = await result.Content.ReadAsStringAsync();

                    if (strresult == "1")
                    {
                        //await PageSO.Instance.LoadSumary();
                        //for (int i = 0; i < 3; i++)
                        //{
                        //    if (Navigation.NavigationStack.Count > 1)
                        //    {
                        //        Page pageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                        //        Navigation.RemovePage(pageToRemove);
                        //    }
                        //}
                        //await Navigation.PopPopupAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                
            }

        }
        public async void clrpgs()
        {
            for (int i = 0; i < 2; i++)
            {
                if (Navigation.NavigationStack.Count > 1)
                {
                    Page pageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Navigation.RemovePage(pageToRemove);
                }
            }
            await Navigation.PopPopupAsync();
        }
        public List<tblMain2Local> tblSavetblMain2LocalNew()
        {

            List<tblMain2Local> listofData = new List<tblMain2Local>();
            foreach (var vl in PageProductList.Instance.listOrders)
            {
                listofData.Add(new tblMain2Local()
                {
                    IC = $"{PagePrevOrder.Instance.mdlTables11.TableDocNum}",
                    StockNumber = vl.StockNumber,
                    PIn = 0,
                    POut = vl.Qty,
                    UP = vl.SellingPrice,
                    Cost = vl.UCost,
                    Discount = 0,
                    Totals = vl.Totals,
                    ProductDesc = vl.ProductDesc,
                    // RowNum = vl.RowNum,
                    OrderTime = DateTime.Now.ToString("hh:mm tt"),
                    Exported = export,
                    DocNumLocal = prevDocNumLocal,
                });
            }
            return listofData;
        }
        private async void SaveNewOrder()
        {
            List<tblMain2Local> listOfData = tblSavetblMain2LocalNew();
            foreach (var item in listOfData)
            {
                int result = await App.ClsServeInsertLocal.SaveMain2(item);

                if (result != 0)
                {
                    await DisplayAlert("alert", result.ToString(), "Ok");
                    return;
                }
                clrpgs1();

            }
        }
        public async void clrpgs1()
        {
            for (int i = 0; i <3; i++)
            {
                if (Navigation.NavigationStack.Count > 1)
                {
                    Page pageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Navigation.RemovePage(pageToRemove);
                }
            }
            await Navigation.PopPopupAsync();
        }
    }
}