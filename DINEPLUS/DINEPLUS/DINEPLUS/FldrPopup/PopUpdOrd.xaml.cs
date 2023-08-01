using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpdOrd : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopUpdOrd()
        {
            InitializeComponent();
            lblPrice.Text = PageViewOrders.Instance.varSP.ToString("n2");
            lblTotal.Text = PageViewOrders.Instance.varSP.ToString("n2");
            txtQty.Text = PageViewOrders.Instance.varQty.ToString();
        }

        private void txtQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtQty.Text == "")
            {
                lblTotal.Text = "0";
            }
            else
            {
                lblTotal.Text = (double.Parse(txtQty.Text) * PageViewOrders.Instance.varSP).ToString("N2");
            }
        }

        private void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            lblTotal.Text = tPrice(PageViewOrders.Instance.varSP, value).ToString("N2");
        }
        public double tPrice(double qty, double prices)
        {
            return qty * prices;
        }
        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (int.Parse(txtQty.Text) <= 0)
            {
                txtQty.Focus();
                await this.DisplayToastAsync("Invalid Quantity");
                return;
            }

            PageViewOrders.Instance.AddOrd(txtQty.Text);
            await Navigation.PopPopupAsync();
        }
    }
}