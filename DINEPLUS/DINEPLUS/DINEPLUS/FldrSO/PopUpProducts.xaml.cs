using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Globalization;
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
            //string text = txtQty.Text.Replace(',', '.'); // Replace comma with period for decimal values

            //if (string.IsNullOrEmpty(text))
            //{
            //    lblTotal.Text = "0";
            //}
            //else
            //{
            //    if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double qty))
            //    {
            //        //lblTotal.Text = $"₱{(qty * PagePAYO.Instance.varSellingPrice).ToString("N2", CultureInfo.InvariantCulture)}";
            //        //lblTotal.Text = $"₱{tPrice(qty * double.Parse(mdlProduct11.SellingPrice)).ToString("N2", CultureInfo.InvariantCulture)}";

            //        double sellingPrice = double.Parse(mdlProduct11.SellingPrice);
            //        double quantity = double.Parse(qty);

            //        double totalPrice = tPrice(quantity, sellingPrice);

            //        lblTotal.Text = $"₱{totalPrice.ToString("N2", CultureInfo.InvariantCulture)}";

            //    }
            //    else
            //    {
            //        lblTotal.Text = "Invalid input"; // Handle invalid input
            //    }
            //}
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
            try
            {
                btnSave.IsEnabled = false;
                if (double.Parse(txtQty.Text) <= 0)
                {
                    txtQty.Focus();
                    await this.DisplayToastAsync("Invalid Quantity");
                    btnSave.IsEnabled = true;

                    return;
                }
                PageProductList.Instance.AddOrdSO(double.Parse(txtQty.Text));
                await Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("alert", ex.ToString(), "Ok");
            }
        }
    }
}