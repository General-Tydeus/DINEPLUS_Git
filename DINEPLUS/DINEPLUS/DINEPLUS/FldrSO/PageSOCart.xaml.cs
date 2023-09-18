using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using DINEPLUSWEBAPI.FldrModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSOCart : ContentPage
    {
        public static PageSOCart Instance;
        public MdlTables mdlTables1;
        public double Totals { get; set; }
        public bool export { get; set; }
        public string prevDocNumLocal { get; set; }

        NetworkAccess current;

        public PageSOCart(MdlTables mdlTables11)
        {
            Instance = this;
            mdlTables1 = mdlTables11;
            InitializeComponent();
            BindingContext = mdlTables11;
        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        public void clsPage2()
        {
            Navigation.RemovePage(this);
        }
        protected async override void OnAppearing()
        {
            try
            {
                LoadSumOrd();
                LoadLV();
            }
            catch (Exception ex)
            {
                await DisplayAlert("alert",ex.ToString(),"oks");
            }
            
        }
        public void LoadLV()
        {
            //LV1.ItemsSource = null;
            if (PageProductList.Instance.listOrders.Count <= 0)
            {
                Navigation.PopAsync();
            }
            LV1.ItemsSource = null;
            LV1.ItemsSource = PageProductList.Instance.listOrders;
        }
        public string LoadSumOrd()
        {
            //Totals = PageProductList.Instance.listOrders.Sum(order => order.Totals);
            //lblTotals.Text = $"₱ {Totals.ToString("N2")}";
            double Totals = PageProductList.Instance.listOrders.Sum(order => order.Totals);
            lblTotals.Text = Totals.ToString("N2");
            return Totals.ToString("N2");
        }

        private async void btnSave_Clicked(object sender, System.EventArgs e)
        {
            btnSave.IsEnabled = false;
            if (PageSO.Instance.Additional == "Additional")
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
                btnSave.IsEnabled = true;
                return;
            }
            await Navigation.PushPopupAsync(new PopUpTakeOrd());
            btnSave.IsEnabled = true;

        }

        private void SwipeItem_Invoked(object sender, System.EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            var obj = item.BindingContext as FldrModel.MdlOrders;
            PageProductList.Instance.listOrders.RemoveAll(x => x.StockNumber == obj.StockNumber);
            PageProductList.Instance.LoadExp();
            LoadSumOrd();
            LoadLV();
        }

        private async void LV1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem is FldrModel.MdlOrders selectedProduct)
            //{
            //    await Navigation.PushPopupAsync(new PageEditOrderSO
            //    {
            //        BindingContext = selectedProduct
            //    });
            //}
            //((ListView)sender).SelectedItem = null;
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
        public List<ModeltblMain2> SavetblMain21()
        {
            var listofData = new List<ModeltblMain2>();
            foreach (var vl in PageProductList.Instance.listOrders)
            {
                listofData.Add(new ModeltblMain2()
                {
                    IC = PagePrevOrder.Instance.mdlTables11.TableDocNum,
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
        public void clrpgs1()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Navigation.NavigationStack.Count > 1)
                {
                    Page pageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                    Navigation.RemovePage(pageToRemove);
                }
            }
        }
    }
}