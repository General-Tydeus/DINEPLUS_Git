using Acr.UserDialogs;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using DINEPLUSWEBAPI.FldrModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrExport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageExport : ContentPage
    {
        public List<tblMain1Local> localMain1 = new List<tblMain1Local>();
        HashSet<string> insertMain1 = new HashSet<string>();
        public bool IsPullToRefreshEnabled { get; set; } = false;
        public string findDocnum { get; set; }
        public string voucher { get; set; }

        public List<tblMain2Local> localMain2;
        NetworkAccess current;


        public PageExport(string vouch)
        {
            InitializeComponent();
            LoadData();
            voucher = vouch;
        }
        public async void LoadData()
        {
            if (voucher == "CS")
            {
                lblTitle.Text = "Export : Pay as you Order";
            }
            else
            {
                lblTitle.Text = "Export : Pay after Order";
            }
            await LoadLV();

        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        public async Task LoadLV()
        {
            localMain1.Clear();
            localMain2 = await App.ClsServeMain.localMain2Exp();

            foreach (var varlist in localMain2)
            {
                if (!insertMain1.Contains(varlist.DocNumLocal))
                {
                    var main2 = await App.ClsServeMain.localMain1Exp(varlist.DocNumLocal, voucher);
                    localMain1.AddRange(main2);

                    insertMain1.Add(varlist.DocNumLocal);
                }
            }
            LV1.ItemsSource = null;
            LV1.ItemsSource = localMain1;
           // await DisplayAlert("ok", localMain1.Count.ToString(), "ok");

        }
        private async void SaveTheTransact()
        {
            using (UserDialogs.Instance.Loading("Exporting..."))
            {
                try
                {
                    foreach (var vl in localMain1)
                    {
                        findDocnum = vl.DocNum;

                        ModeltblMain1 insertMain1 = new ModeltblMain1()
                        {
                            ModelSubtblMain2 = SavetblMain2(vl.DocNum),

                            Voucher = vl.Voucher,
                            UserCode = vl.UserCode,
                            TDate = vl.TDate,
                            DocNum = vl.DocNum,
                            Reference = vl.Reference,
                            ControlNo = vl.ControlNo,
                            Remarks = vl.Remarks,
                            CNCode = vl.CNCode,
                            CashReceived = vl.CashReceived,
                            Serve = vl.Serve,
                            TableCode = vl.TableCode,
                            CAmount = vl.CAmount,
                            GUID = vl.GUID,
                            DocNumLocal = vl.DocNum,
                            Discount = vl.Discount,
                        };
                       // string jsonContent = JsonConvert.SerializeObject(insertMain1);
                        //Console.WriteLine(jsonContent);
                        //await DisplayAlert("one", jsonContent, "ok");

                        using (HttpClient client = new HttpClient())
                        {
                            var content = new StringContent(JsonConvert.SerializeObject(insertMain1), Encoding.UTF8, "application/json");
                            var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/ExportMain1", content);
                            string strresult = await result.Content.ReadAsStringAsync();

                            if (strresult == "1")
                            {
                                // await DisplayAlert("hala", findDocnum, "OK");
                                await Task.Delay(1000);
                                App.ClsServeMain.LocalMain2DeleteAsync(findDocnum);
                                App.ClsServeMain.LocalMain1DeleteAsync(findDocnum);
                                //await DisplayAlert("hala", res2.ToString() + "   :    " + res.ToString(), "OK");
                            }
                            else
                            {
                                await DisplayAlert("Result", strresult, "OK");
                            }

                        }
                    }
                    await LoadLV();
                    await DisplayAlert("Alert", "Succesfully Exported!", "Ok");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "OK");
                }
            }
        }
        public List<ModeltblMain1> tblSavetblMain1()
        {

            var listofData = new List<ModeltblMain1>();
            foreach (var vl in localMain1)
            {

                listofData.Add(new ModeltblMain1()
                {
                    ModelSubtblMain2 = SavetblMain2(vl.DocNum),

                    Voucher = vl.Voucher,
                    UserCode = vl.UserCode,
                    TDate = vl.TDate,
                    DocNum = vl.DocNum,
                    Reference = vl.Reference,
                    ControlNo = vl.ControlNo,
                    Remarks = vl.Remarks,
                    CNCode = vl.CNCode,
                    CashReceived = vl.CashReceived,
                    Serve = vl.Serve,
                    TableCode = vl.TableCode,
                    CAmount = vl.CAmount,
                    GUID = vl.GUID,
                    DocNumLocal = vl.DocNum,
                });
            }
            return listofData;
        }
        public List<ModeltblMain2> SavetblMain2(string docnum)
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

        private async void btnExport_Clicked(object sender, EventArgs e)
        {
      
            btnExport.IsEnabled = false;
            try
            {
                if (localMain1.Count <=0)
                {
                    await DisplayAlert("Alert!", "Nothing to Export", "Ok");
                    return;
                }

                CheckConnection();
                if (current == NetworkAccess.Internet)
                {
                    SaveTheTransact();
                }
                else
                {
                    await DisplayAlert("Alert!", "Must be Connected to the Internet", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error!", ex.ToString(), "Ok");
            }
            btnExport.IsEnabled = true;
        }

    }
}