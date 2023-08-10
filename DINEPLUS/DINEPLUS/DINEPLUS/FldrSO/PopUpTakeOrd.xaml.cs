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
        public string MyDocNum { get; set; }
        public string MyReference { get; set; }



        public PopUpTakeOrd()
        {
            InitializeComponent();
            lblTotals.Text = $"₱ {PageSOCart.Instance.Totals.ToString("n2")}";
        }
        protected async override void OnAppearing()
        {
            int SOMainCount1 = await App.ClsServeMain.SOMainCount();
            SOMainCount1++;
            //MyDocNum = Convert.ToString(SOMainCount1).PadLeft(7, '0');
            MyDocNum = await new ClsAutoNum().GetVoucherAutoNum("SO", PageMainMenu.Instance.strCNCode);
        }

        private void btnClose_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private async void BtnPost_Clicked(object sender, System.EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txtRemarks.Text))
            {
                await DisplayAlert("info","please complete entry!","OK");
                return;
            }

            if (PageSO.Instance.Additional == "New")
            {
                SaveTheTransact();
            }
            else if (PageSO.Instance.Additional == "Additional")
            {
                SaveAdditional();
            }
            else if (PageSO.Instance.Additional == "")
            {
                //SaveAdditional();
                await DisplayAlert("Additional is empty", PageSO.Instance.Additional, "OK");
            }

            //else
            //{
            //        await App.ClsServeMain.SaveClsModelSO1(tblSavetblMain1());
            //        await App.ClsServeMain.SaveClsModelSO2(SavetblMain2());

            //        App.ClsServeMain.TableOccupied(tblSavetblMain1().TableCode, MyDocNum);

            //        return;
            //}



        }

        public ModeltblMain1 tblSavetblMain1()
        {
            return new ModeltblMain1()
            {
                ModelSubtblMain2 = SavetblMain2(),

                //IC = $"SO{MyDocNum}",
                Voucher = "SO",
                DocNum = MyDocNum,
                UserCode = PageMainMenu.Instance.strUserCode,
                TDate = DateTime.Now.ToString("MM,dd,yyyy"),
                Reference = $"SO{MyDocNum}",
                ControlNo = "001",
                Remarks = txtRemarks.Text,
                CNCode = PageMainMenu.Instance.strCNCode,
                TableCode = PageProductList.Instance.MdlTables11.TableCode,
                CashReceived = 0,
                Serve = false,
                //TableDesc = PageProductList.Instance.MdlTables11.TableDesc,
                CAmount = 0
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

        //public LocaltblMain1 tblSavetblMain1()
        //{
        //    return new LocaltblMain1()
        //    {
        //        IC = $"SO{MyDocNum}",
        //        Voucher = "SO",
        //        DocNum = MyDocNum,
        //        UserCode = PageMainMenu.Instance.strUserCode,
        //        TDate = DateTime.Now,
        //        Reference = $"SO{MyDocNum}",
        //        ControlNo = "001",
        //        Remarks = txtRemarks.Text,
        //        CNCode = PageMainMenu.Instance.strCNCode,
        //        TableCode = PageProductList.Instance.MdlTables11.TableCode,
        //        TableDesc = PageProductList.Instance.MdlTables11.TableDesc,
        //        CAmount = PageSOCart.Instance.Totals
        //    };
        //}



        //public List<LocaltblMain2> SavetblMain2()
        //{
        //    var listofData = new List<LocaltblMain2>();
        //    foreach (var vl in PageProductList.Instance.listOrders)
        //    {
        //        listofData.Add(new LocaltblMain2()
        //        {
        //            IC = $"SO{MyDocNum}",
        //            DocNum = MyDocNum,
        //            StockNumber = vl.StockNumber,
        //            PIn = 0,
        //            POut = vl.Qty,
        //            UP = vl.SellingPrice,
        //            Cost = vl.UCost,
        //            Discount = 0,
        //            ProductDesc = vl.ProductDesc,
        //            Totals = vl.Totals,
        //            OrderTime = DateTime.Now.ToString("hh:mm tt")
        //        });
        //    }
        //    return listofData;
        //}


        private async void SaveTheTransact()
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(tblSavetblMain1()), Encoding.UTF8, "application/json");
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Voucher/InsertMain1", content);

                string strresult = await result.Content.ReadAsStringAsync();

               // await DisplayAlert("Notif", strresult, "OK");

                if (strresult == "1")
                {
                    var clientGet = new HttpClient();
                    clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Various/GetDoorMessage/?strWAPIVoucher=SO");
                    HttpResponseMessage response = await clientGet.GetAsync("");
                    strresult = await response.Content.ReadAsStringAsync();
                    string strresultFinal = strresult.Trim('"');
                    if (strresultFinal == "0")
                    {
                        await UpdateTblStatus();
                        //PageSOCart.Instance.clsPage2();
                        ////
                        ////PageSO.Instance.docnum = MyDocNum;
                        ////PageProductList.Instance.clsPage1();

                        //await PageSO.Instance.LoadSumary();
                        for (int i = 0; i < 2; i++)
                        {
                            if (Navigation.NavigationStack.Count > 1)
                            {
                                Page pageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                                Navigation.RemovePage(pageToRemove);
                            }
                        }
                        //await Navigation.PopAsync();
                        await Navigation.PopPopupAsync();
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

        public async Task UpdateTblStatus()
        {
            ModeltblMain1 ModeltblMain11 = new ModeltblMain1()
            {
                TableCode = PageProductList.Instance.MdlTables11.TableCode,
                TableDesc = "O",
                TableDocNum = $"SO{MyDocNum}{PageMainMenu.Instance.strCNCode}",
            };
            var json = JsonConvert.SerializeObject(ModeltblMain11);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var result = await client.PutAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/UpdateTblStatus", content);
            string strresult = await result.Content.ReadAsStringAsync();
            //await DisplayAlert("Notif", strresult, "OK");
        }
        public async void SaveAdditional()
        {
            //await DisplayAlert("IC", text, "OK");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                     //modelMain2 = SavetblMain2();
                    //string strIC = PagePrevOrder.Instance.mdlTables11.TableDocNum;



                    var content = new StringContent(JsonConvert.SerializeObject(SavetblMain21()), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Voucher/InsertMain2Additional", content);
                    string strresult = await result.Content.ReadAsStringAsync();

                    if (strresult == "1")
                    {
                        //PageSOCart.Instance.clsPage2();
                        //PageProductList.Instance.clsPage1();
                        //PageSO.Instance.docnum = MyDocNum;
                        // PageProductList.Instance.clsPage1();
                        await PageSO.Instance.LoadSumary();
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
                    //await DisplayAlert("SaveAdditional", strresult, "OK");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}