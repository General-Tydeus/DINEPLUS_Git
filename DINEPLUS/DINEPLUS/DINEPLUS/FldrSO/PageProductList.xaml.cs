using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageProductList : ContentPage
    {
        public MdlTables MdlTables11;
        public MdlProduct mdlProduct1;
        public List<MdlOrders> listOrders = new List<MdlOrders>();
        public static PageProductList Instance;

        public PageProductList(MdlTables mdlTables1)
        {
            Instance = this;
            MdlTables11 = mdlTables1;
            InitializeComponent();
            BindingContext = mdlTables1;
        }
        protected async override void OnAppearing()
        {
            var varlist = await new ClsListEntry().GetProductList();
            ClMenu.ItemsSource = varlist;
        }


        public void clsPage1()
        {
            Navigation.RemovePage(this);
        }

        private void ClMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mdlProduct1 = e.CurrentSelection[0] as MdlProduct;

            Navigation.PushPopupAsync(new PopUpProducts(mdlProduct1));
        }


        public void AddOrdSO(int intQty)
        {
            var counts = listOrders.Count;
            if (mdlProduct1 != null)
            {
                var item = listOrders.FirstOrDefault(x => x.StockNumber == mdlProduct1.StockNumber);
                if (item == null)
                {
                    listOrders.Add(new MdlOrders()
                    {
                        StockNumber = mdlProduct1.StockNumber,
                        ProductDesc = mdlProduct1.ProductDesc,
                        SellingPrice = mdlProduct1.SellingPrice,
                        Qty = intQty,
                        RowNum = ++counts,
                        Totals = mdlProduct1.SellingPrice * intQty,
                        UCost = mdlProduct1.UCost,
                        UnitMeasure = mdlProduct1.UnitMeasure,
                    });
                }
                else
                {
                    listOrders.RemoveAll(x => x.StockNumber == mdlProduct1.StockNumber);
                    listOrders.Add(new MdlOrders()
                    {
                        StockNumber = mdlProduct1.StockNumber,
                        ProductDesc = mdlProduct1.ProductDesc,
                        SellingPrice = mdlProduct1.SellingPrice,
                        Qty = intQty + item.Qty,
                        RowNum = item.RowNum,
                        Totals = item.Totals + (mdlProduct1.SellingPrice * intQty),
                        UCost = mdlProduct1.UCost,
                        UnitMeasure = mdlProduct1.UnitMeasure
                    });
                }
            }
            LoadExp();
        }

        public void LoadExp()
        {
            lblCartOrder.Text = listOrders.Count.ToString();
        }

        private void btnCart_Clicked(object sender, System.EventArgs e)
        {
            if(listOrders.Count <= 0)
            {
                this.DisplayToastAsync("No Item to Show!!", 500);
                return;
            }
            Navigation.PushAsync(new PageSOCart(MdlTables11));
        }
    }
}