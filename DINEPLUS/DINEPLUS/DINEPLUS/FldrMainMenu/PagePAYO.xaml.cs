using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using DINEPLUS.FldrPopup;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrMainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePAYO : ContentPage
    {
        public static PagePAYO Instance;

        public double varSellingPrice { get; set; }
        public double dblTotals { get; set; }
        public int strCartCount { get; set; }
        NetworkAccess current;

        public List<MdlOrders> listOrders = new List<MdlOrders>();
        public MdlProduct obj = new MdlProduct();
        public MdlCategory cat = new MdlCategory();
        List<MdlProduct> productList;
        List<MdlProduct> filteredList;

        public PagePAYO()
        {
            InitializeComponent();
            Instance = this;
            current = Connectivity.NetworkAccess;
        }

        protected async override void OnAppearing()
        {
                ClMenu.ItemsSource = await App.ClsServeMain.ImportProductList();
                ClCat.ItemsSource = await App.ClsServeMain.ImportCategoryList();

                productList = await App.ClsServeMain.ImportProductList();
        }

        private async void ClMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                obj = e.CurrentSelection[0] as MdlProduct;
                if (obj != null)
                {
                    varSellingPrice = obj.SellingPrice;
                    await Navigation.PushPopupAsync(new PageAddOrder
                    {
                        BindingContext = e.CurrentSelection[0] as MdlProduct
                    });

                    ((CollectionView)sender).SelectedItem = null;
                }
            }
        }
        public void AddOrd(double intQty)
        {
            var counts = listOrders.Count;
            if (obj != null)
            {
                var item = listOrders.FirstOrDefault(x => x.StockNumber == obj.StockNumber);
                if (item == null)
                {
                    listOrders.Add(new MdlOrders()
                    {
                        StockNumber = obj.StockNumber,
                        ProductDesc = obj.ProductDesc,
                        UnitMeasure = obj.UnitMeasure,
                        SellingPrice = obj.SellingPrice,
                        Qty = intQty,
                        RowNum = ++counts,
                        Totals = obj.SellingPrice * intQty,
                        UCost = obj.UCost,
                    });
                }
                else
                {
                    listOrders.RemoveAll(x => x.StockNumber == obj.StockNumber);
                    listOrders.Add(new MdlOrders()
                    {
                        StockNumber = obj.StockNumber,
                        ProductDesc = obj.ProductDesc,
                        SellingPrice = obj.SellingPrice,
                        Qty = intQty + item.Qty,
                        RowNum = item.RowNum,
                        Totals = item.Totals + (obj.SellingPrice * intQty),
                        UCost = obj.UCost,
                        UnitMeasure = obj.UnitMeasure
                    });
                }
            }
            LoadExp();
            focusme.Focus();
        }
        public void LoadExp()
        {
            lblCartOrder.Text = listOrders.Count.ToString();
            //lblCartCount.Text = strExpCount.ToString();
        }
        private void btnCart_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert(" ", "   ", listOrders.Count.ToString());
            if (listOrders.Count <= 0)
            {
                DisplayAlert("Attention", "No Order To Show", "Ok");
                return;
            }
            Navigation.PushAsync(new PageViewOrders());
        }

        private void ClCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.CurrentSelection.Any())
            {

                var selectedItem = (MdlCategory)e.CurrentSelection.FirstOrDefault();

                if (selectedItem != null)
                {
                    string CatCode = selectedItem.CatCode;

                    filteredList = productList.Where(item => item.CatCode == CatCode).ToList();
                    ClMenu.ItemsSource = filteredList;
                }
                //ClCat.SelectedItem = null;
            }
                

        }
    }
}