using DINEPLUS.FldrClass;
using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageViewOrders : ContentPage
    {
        //public string strCode { get; set; }
        //public string strDesc { get; set; }
        //public double strPrice { get; set; }
        //public double strQty { get; set; }

        public string strDisc;
        public static PageViewOrders Instance;
        public List<FldrModel.MdlOrders> listOrders = new List<FldrModel.MdlOrders>();
        public MdlProduct obj = new MdlProduct();
        NetworkAccess current;
        public PageViewOrders()
        {
            InitializeComponent();
            Instance = this;

        }
        public void CheckConnection()
        {
            current = Connectivity.NetworkAccess;
        }
        protected override void OnAppearing()
        {
            LoadLV();
            //UpdateTotal();
            LoadSumOrd();
            CheckConnection();
            GetDiscount();
        }

        public async void GetDiscount()
        {
            //strDisc = await new ClsGetSomething().GetDiscount();
            MdlDiscount discount = await App.ClsServeMain.ImportDiscount();
            if (discount != null)
            {
                strDisc = discount.Discount;
            }
            //if (current == NetworkAccess.Internet)
            //{
            //    strDisc = await new ClsGetSomething().GetDiscount();
            //}
            //else
            //{
            //    MdlDiscount discount = await App.ClsServeMain.ImportDiscount();
            //    if (discount != null)
            //    {
            //        strDisc = discount.Discount;
            //    }
            //    else
            //    {
            //        strDisc = "No discount available.";
            //    }
            //}
           // await DisplayAlert("one", strDisc, "oks");
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

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PagePayOrder(), true);
            return;
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            var obj = item.BindingContext as FldrModel.MdlOrders;
            PagePAYO.Instance.listOrders.RemoveAll(x => x.StockNumber == obj.StockNumber);
            PagePAYO.Instance.LoadExp();
            LoadSumOrd();
            LoadLV();
        }

        private async void LV1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is FldrModel.MdlOrders selectedProduct)
            {
                //await DisplayAlert("qty", selectedProduct.Qty.ToString(), "ok");
                await Navigation.PushPopupAsync(new PageEditOrder
                {
                    BindingContext = selectedProduct
                });
            }
            ((ListView)sender).SelectedItem = null;
        }
      
    }
}