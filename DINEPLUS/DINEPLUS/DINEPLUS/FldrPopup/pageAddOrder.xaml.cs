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

namespace DINEPLUS.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAddOrder : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PageAddOrder()
        {
            InitializeComponent();
            lblPrice.Text = PagePAYO.Instance.varSellingPrice.ToString("n2");
            lblTotal.Text = PagePAYO.Instance.varSellingPrice.ToString("n2");
        }

        private void txtQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtQty.Text == "")
            {
                lblTotal.Text = "0";
            }
            else
            {
                lblTotal.Text = (double.Parse(txtQty.Text) * PagePAYO.Instance.varSellingPrice).ToString("N2");
            }
        }

        private void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            lblTotal.Text = tPrice(value, PagePAYO.Instance.varSellingPrice).ToString("N2");

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
            PagePAYO.Instance.AddOrd(int.Parse(txtQty.Text));
            await Navigation.PopPopupAsync();
        }
    }
}