using DINEPLUS.FldrModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSOCart : ContentPage
    {
        MdlTables mdlTables1;

        public PageSOCart(MdlTables mdlTables11)
        {
            mdlTables1 = mdlTables11;
            InitializeComponent();
            BindingContext = mdlTables11;
        }

        protected override void OnAppearing()
        {
            LoadSumOrd();
            LoadLV();
        }
        public void LoadLV()
        {
            LV1.ItemsSource = null;
            if (PageProductList.Instance.listOrders.Count <= 0)
            {
                Navigation.PopAsync();
            }
            LV1.ItemsSource = PageProductList.Instance.listOrders;
        }
        public void LoadSumOrd()
        {
            double Totals = PageProductList.Instance.listOrders.Sum(order => order.Totals);
            lblTotals.Text = $"₱ {Totals.ToString("N2")}";
        }
    }
}