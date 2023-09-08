using DINEPLUS.FldrModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpProducts : PopupPage
    {
        MdlProduct mdlProduct11;
        public PopUpProducts(MdlProduct mdlProduct1)
        {
            mdlProduct11 = mdlProduct1;
            InitializeComponent();
            BindingContext = mdlProduct1;
        }

        private void txtQty_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

        }

        private void stepper_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            lblTotal.Text = $"₱ {tPrice(value, mdlProduct11.SellingPrice).ToString("N2")}";

        }
        public double tPrice(double qty, double prices)
        {
            return qty * prices;
        }

        private async void btnSave_Clicked(object sender, System.EventArgs e)
        {
            btnSave.IsEnabled = false;
            if (int.Parse(txtQty.Text) <= 0)
            {
                txtQty.Focus();
                await this.DisplayToastAsync("Invalid Quantity");
                return;
            }
            PageProductList.Instance.AddOrdSO(int.Parse(txtQty.Text));
            await Navigation.PopPopupAsync();
        }
    }
}