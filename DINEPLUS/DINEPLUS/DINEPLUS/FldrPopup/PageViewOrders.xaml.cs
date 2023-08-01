using DINEPLUS.FldrMainMenu;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DINEPLUS.FldrModel.ClsModelMain;

namespace DINEPLUS.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageViewOrders : ContentPage
    {
        public double varQty { get; set; }
        public double varSP { get; set; }
        public double dblTotals { get; set; }
        public int strCartCount { get; set; }
        public static PageViewOrders Instance;

        public MdlOrders obj = new MdlOrders();

        public PageViewOrders()
        {
            InitializeComponent();
            Instance = this;

            //SetTabEnabled(PageMainMenu., false);
            //SetTabEnabled(soPage, false);
            //SetTabEnabled(PagePAYO, false);,

        }
        // Method to disable all tabs' content pages
        
        protected override void OnAppearing()
        {
            PageMainMenu.Instance.DisableAllTabs();
            LoadSumOrd();
            LoadLV();
        }
        public void LoadLV()
        {
            if (PagePAYO.Instance.listOrders.Count <= 0)
            {
                Navigation.PopAsync();
            }
            LV1.ItemsSource = null;
            LV1.ItemsSource = PagePAYO.Instance.listOrders;
        }
        public string LoadSumOrd()
        {
            double Totals = PagePAYO.Instance.listOrders.Sum(order => order.Totals);
            lblTotals.Text = Totals.ToString("N2");
            return Totals.ToString("N2");
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public void AddOrd(string strQty)
        {
            int qty = int.Parse(strQty);

            PagePAYO.Instance.listOrders.RemoveAll(x => x.StockNumber == obj.StockNumber);
            PagePAYO.Instance.listOrders.Add(new MdlOrders()
            {
                StockNumber = obj.StockNumber,
                ProductDesc = obj.ProductDesc,
                SellingPrice = obj.SellingPrice,
                UnitMeasure = obj.UnitMeasure,
                Qty = qty,
                RowNum = obj.RowNum,
                Totals = obj.SellingPrice * qty,
                UCost = obj.UCost,
            });
            LoadSumOrd();
            LoadLV();
        }
        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PagePayOrder(), true);
            return;
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            var Vals = item.BindingContext as MdlOrders;
            PagePAYO.Instance.listOrders.RemoveAll(x => x.StockNumber == Vals.StockNumber);
            LoadSumOrd();
            LoadLV();
            PagePAYO.Instance.LoadExp();
        }

        private async void LV1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            obj = e.SelectedItem as MdlOrders;
            varQty = obj.Qty;
            varSP = obj.SellingPrice;
            await Navigation.PushPopupAsync(new PopUpdOrd
            {
                BindingContext = e.SelectedItem as MdlOrders
            });
            //StrInfo = e.SelectedItem as MdlProduct;
            //varQty = Convert.ToInt32(StrInfo.Qty);
            //varSPPC1 = StrInfo.SPPC1;
            //await Navigation.PushPopupAsync(new PopUpdOrd
            //{
            //    BindingContext = e.SelectedItem as MdlOrders
            //});
        }
    }
}