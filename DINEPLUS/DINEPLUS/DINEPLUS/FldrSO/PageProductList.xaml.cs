using DINEPLUS.FldrClass;
using DINEPLUS.FldrModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageProductList : ContentPage
    {
        public PageProductList(MdlTables mdlTables1)
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            var varlist = await new ClsListEntry().GetProductList();

            ClMenu.ItemsSource = varlist;
        }

        private void ClMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}