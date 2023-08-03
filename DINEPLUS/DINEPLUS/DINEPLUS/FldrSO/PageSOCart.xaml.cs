using DINEPLUS.FldrModel;
using Rg.Plugins.Popup.Extensions;
using System.Linq;
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


        public PageSOCart(MdlTables mdlTables11)
        {
            Instance = this;
            mdlTables1 = mdlTables11;
            InitializeComponent();
            BindingContext = mdlTables11;
        }
        public void clsPage2()
        {
            Navigation.RemovePage(this);
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
            Totals = PageProductList.Instance.listOrders.Sum(order => order.Totals);

             
            lblTotals.Text = $"₱ {Totals.ToString("N2")}";
        }

        private async void btnSave_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new PopUpTakeOrd());
        }
    }
}