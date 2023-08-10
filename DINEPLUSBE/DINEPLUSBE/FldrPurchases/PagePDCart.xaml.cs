using DINEPLUSBE.FldrClass;
using DINEPLUSBE.FldrPurchases;
using DINEPLUSBE.FldrModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using DINEPLUSBE.FldrLoginPage;
using DINEPLUSBE.FldrControlPanel;

namespace DINEPLUSBE.FldrPurchases
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePDCart : ContentPage
    {
        private int selectedIndex;
        private string pristrUserCode;
        public PagePDCart()
        {
            InitializeComponent();
            LV1.ItemSelected += LV1_ItemSelected;
        }

        private void LV1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                ModeltblMain2PD selectedItem = LV1.SelectedItem as ModeltblMain2PD;
                selectedIndex = PagePDProductSearchList.Instance.ModeltblMain2PDList.IndexOf(selectedItem);
                btnDelete.IsVisible = true;
            }
            catch (Exception)
            {
                DisplayAlert("Information", "Something went wrong. Possible error in connection", "OK");
            }
        }

        protected async override void OnAppearing()
        {
            try
            {
                var varUserName = await App.ClsServeMain.GetCurrentUser();
                pristrUserCode = varUserName.UserCode;

                var ItemInListView = PagePDProductSearchList.Instance.ModeltblMain2PDList;
                LV1.ItemsSource = ItemInListView;
                double dblTotalAmount = 0;
                foreach (var varlooplist in ItemInListView)
                {
                    dblTotalAmount = dblTotalAmount + (varlooplist.PIn * varlooplist.UCost);
                }
                lblEntTotalAmt.Text = dblTotalAmount.ToString("N2");
            }

            catch (Exception)
            {
                await DisplayAlert("Information", "Something is wrong, possible error in connection", "OK");
            }
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(tblSavetblMain1()), Encoding.UTF8, "application/json");
                var result = await client.PostAsync($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Voucher/InsertMain1", content);
                if (result.IsSuccessStatusCode)
                {
                    var clientGet = new HttpClient();
                    clientGet.BaseAddress = new Uri($"{new ClsGetIPAddress().GetIPAddress()}/API/DINEPLUSWEBAPI/Various/GetDoorMessage/?strWAPIVoucher=PD");

                    HttpResponseMessage response = await clientGet.GetAsync("");
                    string strresult = await response.Content.ReadAsStringAsync();
                    string strresultFinal = strresult.Trim('"');

                    if (strresultFinal == "0")
                    {
                        await DisplayAlert("Information", "Saved", "OK");
                        await Navigation.PushAsync(new PageMainMenu());
                        var currenPage = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                        var pageList = Navigation.NavigationStack.Where(y => y != currenPage).ToList();
                        foreach (var page in pageList)
                            Navigation.RemovePage(page);
                    }
                    else if (strresultFinal == "1")
                    {
                        await DisplayAlert("Information", "Transaction not saved", "OK");
                    }
                    else if (strresultFinal == "2")
                    {
                        await DisplayAlert("Information", "Contact your administrator", "OK");
                    }
                    else if (strresultFinal == "3")
                    {
                        await DisplayAlert("Information", "Transaction not saved", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Information", "Failed to save", "OK");
                }
            }

        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {
            PagePDProductSearchList.Instance.ModeltblMain2PDList.RemoveAt(selectedIndex);
            LV1.ItemsSource = null;
            OnAppearing();
            LV1.Focus();
            btnDelete.IsVisible = false;
        }

        public ModeltblMain1 tblSavetblMain1()
        {
            return new ModeltblMain1()
            {
                Voucher = "PD",
                UserCode = pristrUserCode,
                TDate = PagePDName.Instance.DPTDate.Date.ToString("MM/dd/yyyy"),
                Reference = PagePDName.Instance.txtReference.Text,
                ControlNo = PagePDName.Instance.pubstrControlNo,
                Remarks = "Purchases",
                Void = true,
                CNCode = "01",
                CashReceived = 0,
                Serve = true,
                TableCode = "00",
                CAmount=0,
                ModelSubtblMain2 = SavetblMain2()
            };
        }

        public List<ModeltblMain2> SavetblMain2()
        {
            int intRowNum = 1;
            var listofData = new List<ModeltblMain2>();
            foreach (var varlooplist in PagePDProductSearchList.Instance.ModeltblMain2PDList)
            {
                listofData.Add(new ModeltblMain2()
                {
                    StockNumber = varlooplist.StockNumber,
                    PIn = varlooplist.PIn,
                    POut = 0,
                    UP = varlooplist.UCost,
                    Cost = varlooplist.Total,
                    Discount = 0,
                    RowNum = intRowNum++,
                });
            }
            return listofData;
        }
    }
}

