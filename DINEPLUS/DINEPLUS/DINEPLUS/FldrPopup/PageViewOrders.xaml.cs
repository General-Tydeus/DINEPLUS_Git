using DINEPLUS.FldrMainMenu;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DINEPLUS.FldrPopup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageViewOrders : ContentPage
    {
        public static PageViewOrders Instance;
        public PageViewOrders()
        {
            InitializeComponent();
            Instance = this;

        }
        protected override void OnAppearing()
        {
            LoadSumOrd();
            LoadLV();
        }
        public void LoadLV()
        {
            if (PagePAYO.Instance.listOrders.Count <= 0)
            {
                Navigation.PopAsync();
            }
            LV1.ItemsSource = null;
            LV1.ItemsSource = PagePAYO.Instance.listOrders;
        }
        public string LoadSumOrd()
        {
            double Totals = PagePAYO.Instance.listOrders.Sum(order => order.Totals);
            lblTotals.Text = Totals.ToString("N2");
            return Totals.ToString("N2");
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PagePayOrder(), true);
            return;
        }
    }
}