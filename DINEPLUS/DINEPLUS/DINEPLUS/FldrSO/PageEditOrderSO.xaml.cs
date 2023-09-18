using DINEPLUS.FldrMainMenu;
using DINEPLUS.FldrModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrSO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEditOrderSO : Rg.Plugins.Popup.Pages.PopupPage
    {
        double origQty;
        public PageEditOrderSO()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            try
            {
                origQty = double.Parse(txtQty.Text);
                stepper.Minimum = -1 * double.Parse(txtQty.Text);
                lblTotal.Text = $"₱ {double.Parse(txtQty.Text) * double.Parse(lblPrice.Text)}";
            }
            catch (Exception ex)
            {
                await DisplayAlert("alert", ex.ToString(), "oks");
            }
        }
        private async void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                double newValue = e.NewValue;
                double oldValue = e.OldValue;
                if (newValue > oldValue)
                {
                    txtQty.Text = (double.Parse(txtQty.Text) + 1).ToString();
                }
                else if (newValue < oldValue)
                {
                    if (double.Parse(txtQty.Text) <= 1)
                    {
                        //await DisplayAlert("Notification", "Quantity must not be less than 1", "Ok");
                        return;
                    }
                    txtQty.Text = (double.Parse(txtQty.Text) - 1).ToString();
                }
                else
                {
                    await DisplayAlert("Notification", "We're here", "Ok");
                }
                lblTotal.Text = tPrice(double.Parse(txtQty.Text), double.Parse(lblPrice.Text)).ToString("N2");
            }
            catch (Exception ex)
            {
                await DisplayAlert("alert", ex.ToString(), "oks");
            }
            

        }
        public double tPrice(double qty, double prices)
        {
            return qty * prices;
        }
        private void txtQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtQty.Text.Replace(',', '.'); // Replace comma with period for decimal values

            if (string.IsNullOrEmpty(text))
            {
                lblTotal.Text = "0";
            }
            else
            {
                if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double qty))
                {
                    lblTotal.Text = $"₱{(qty * PagePAYO.Instance.varSellingPrice).ToString("N2", CultureInfo.InvariantCulture)}";
                }
                else
                {
                    lblTotal.Text = "Invalid input"; // Handle invalid input
                }
            }
        }

        private async void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            //bool userClickedYes = await DisplayAlert("Alert", "Confirm Changes?", "YES","NO");
            //if (userClickedYes)
            //{
            //    updateData();
            //    PageSOCart.Instance.LoadLV();
            //    PageSOCart.Instance.LoadSumOrd();
            //    await PopupNavigation.Instance.PopAsync();
            //}
            //else
            //{
                txtQty.Text = origQty.ToString();
                updateData();
                PageSOCart.Instance.LoadLV();
                PageSOCart.Instance.LoadSumOrd();
                await PopupNavigation.Instance.PopAsync();
            //}
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            updateData();
            PageSOCart.Instance.LoadLV();
            PageSOCart.Instance.LoadSumOrd();
            await PopupNavigation.Instance.PopAsync();
        }

        public void updateData()
        {
            foreach (var item in PageProductList.Instance.listOrders)
            {
                item.Totals = item.Qty * item.SellingPrice;
            }

        }
    }
}