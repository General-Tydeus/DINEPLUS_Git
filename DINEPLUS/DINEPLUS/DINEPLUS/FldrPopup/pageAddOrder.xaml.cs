using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;
using DINEPLUS.FldrMainMenu;
using Xamarin.CommunityToolkit.Extensions;
using System.Globalization;

namespace DINEPLUS.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAddOrder : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PageAddOrder()
        {
            InitializeComponent();
            lblPrice.Text = $"₱{PagePAYO.Instance.varSellingPrice.ToString("n2")}";
            lblTotal.Text = $"₱{PagePAYO.Instance.varSellingPrice.ToString("n2")}";
            txtQty.Text = $"{1}";
        }

        private void txtQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtQty.Text.Replace(',', '.'); 

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
                    lblTotal.Text = "Invalid input"; 
                }
            }
        }

        private void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            lblTotal.Text = $"₱{tPrice(value, PagePAYO.Instance.varSellingPrice).ToString("N2")}";

        }
        public double tPrice(double qty, double prices)
        {
            return qty * prices;
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
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
                PagePAYO.Instance.AddOrd(double.Parse(txtQty.Text));
                await Navigation.PopPopupAsync();
                btnSave.IsEnabled = true;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert",ex.ToString(),"ok");
            }
        }

    }
}